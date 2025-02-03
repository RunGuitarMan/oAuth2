using OAuth2.Core.Interfaces;

namespace OAuth2.Core.Configuration
{
    public class OAuth2Configuration : IOAuth2Configuration
    {
        public string Issuer { get; }
        public string Audience { get; }
        public int AccessTokenLifetime { get; }
        public int RefreshTokenLifetime { get; }
        public string SigningKey { get; }

        public OAuth2Configuration(OAuth2Options options)
        {
            Issuer = options.Issuer;
            Audience = options.Audience;
            AccessTokenLifetime = options.AccessTokenLifetime;
            RefreshTokenLifetime = options.RefreshTokenLifetime;
            SigningKey = options.SigningKey;
        }
    }
}