namespace Mock.AuthService.Models
{
    public class JwtSettings
    {
        public required string Key { get; set; } = string.Empty;
        public required string Issuer { get; set; } = string.Empty;
        public required string Audience { get; set; } = string.Empty;
        public required int ExpireMinutes { get; set; } = 1;
    }
}
