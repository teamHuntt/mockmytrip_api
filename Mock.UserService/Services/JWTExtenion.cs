using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;

public static class JWTExtenion
{
    public static async Task<IServiceCollection> AddJwtAuthenticationFromVaultAsync(
        this IServiceCollection services,
        string issuer,
        string audience)
    {
        // Get Vault info from environment
        var vaultAddr = Environment.GetEnvironmentVariable("VAULT_ADDR") ?? "http://127.0.0.1:8200";
        var vaultToken = Environment.GetEnvironmentVariable("VAULT_TOKEN")
                         ?? throw new InvalidOperationException("VAULT_TOKEN not set");

        // Create Vault client
        var authMethod = new TokenAuthMethodInfo(vaultToken);
        var vaultClientSettings = new VaultClientSettings(vaultAddr, authMethod);
        var vaultClient = new VaultClient(vaultClientSettings);

        // Fetch JWT key
        var secret = await vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync("jwt");
        if (secret?.Data?.Data == null || !secret.Data.Data.ContainsKey("Jwt__Key"))
            throw new InvalidOperationException("JWT key not found in Vault.");

        var jwtKey = secret.Data.Data["Jwt__Key"]?.ToString();
        if (string.IsNullOrWhiteSpace(jwtKey))
            throw new InvalidOperationException("JWT key is empty in Vault.");

        // Configure JWT Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
            };
        });

        services.AddAuthorization();

        return services;
    }
}
