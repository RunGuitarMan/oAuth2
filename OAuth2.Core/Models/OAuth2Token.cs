using System;
using System.Collections.Generic;

namespace OAuth2.Core.Models
{
    public class OAuth2Token
    {
        public string TokenId { get; set; } // Уникальный идентификатор токена
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string TokenType { get; set; } = "Bearer";
        public IEnumerable<OAuth2Scope> Scopes { get; set; }
        public string ClientId { get; set; }
        public string UserId { get; set; }
    }
}