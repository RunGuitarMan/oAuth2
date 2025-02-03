namespace OAuth2.Core.Interfaces
{
    public interface IOAuth2Configuration
    {
        string Issuer { get; }
        string Audience { get; }
        int AccessTokenLifetime { get; }
        int RefreshTokenLifetime { get; }
        string SigningKey { get; }
        // Дополнительные параметры при необходимости
    }
}