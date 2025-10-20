using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;

namespace Mock.UserService.Services
{
    public class VaultService
    {
        private readonly IVaultClient _vaultClient;
        public VaultService()
        {
            var vaultAddr = Environment.GetEnvironmentVariable("VAULT_ADDR") ?? "http://127.0.0.1:8200";
            var vaultToken = Environment.GetEnvironmentVariable("VAULT_TOKEN") ?? throw new InvalidOperationException("VAULT_TOKEN not set");

            var authMethod = new TokenAuthMethodInfo(vaultToken);
            var vaultClientSettings = new VaultClientSettings(vaultAddr, authMethod);

            _vaultClient = new VaultClient(vaultClientSettings);
        }
        public async Task<string> GetJwtKeyAsync()
        {
            var secret = await _vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync("jwt");

            if (secret?.Data?.Data == null || !secret.Data.Data.ContainsKey("Jwt__Key"))
                throw new InvalidOperationException("JWT key not found in Vault.");

            var key = secret.Data.Data["Jwt__Key"]?.ToString();

            if (string.IsNullOrWhiteSpace(key))
                throw new InvalidOperationException("JWT key is empty in Vault.");

            return key;
        }
    }
}
