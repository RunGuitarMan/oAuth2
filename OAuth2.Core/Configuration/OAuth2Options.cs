namespace OAuth2.Core.Configuration
{
    public class OAuth2Options
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessTokenLifetime { get; set; } = 3600; // секунд
        public int RefreshTokenLifetime { get; set; } = 86400; // секунд
        public string SigningKey { get; set; }
        // Дополнительные параметры при необходимости
    }
}