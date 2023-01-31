namespace UniversityApiBackend.Models
{
    public class JwtSettings
    {
        public bool ValidateIssuerSigninKey { get; set; }
        public string IssuerSigningKey { get; set; } = string.Empty;

        public bool ValidateIssuer { get; set; } = true;
        public string? ValidIssuer { get; set; }

        public bool ValidateAudience { get; set; }
        public string? ValidAudience { get; set;}

        public bool RequireExpirationTime { get; set; } = true;
        public bool ValidateLifeTime { get; set; } = true;
    }
}
