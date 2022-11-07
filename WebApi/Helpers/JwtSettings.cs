namespace WebApi.Helpers
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public string ExpirationInDays { get; set; }
    }
}
