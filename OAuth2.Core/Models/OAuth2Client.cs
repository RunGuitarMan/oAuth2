using System.Collections.Generic;

namespace OAuth2.Core.Models
{
    public class OAuth2Client
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public IEnumerable<string> RedirectUris { get; set; }
        public IEnumerable<GrantType> AllowedGrantTypes { get; set; }
        public IEnumerable<OAuth2Scope> AllowedScopes { get; set; }
    }
}