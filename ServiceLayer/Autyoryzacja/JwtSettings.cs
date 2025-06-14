public class JwtSettings
{
    public string SecretKey { get; set; } = null!;
    public string Issuer { get; set; } = "example-api";
    public string Audience { get; set; } = "example-client";
    public int ExpiryMinutes { get; set; } = 60;
}
