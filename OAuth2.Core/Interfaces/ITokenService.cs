using System.Collections.Generic;
using System.Threading.Tasks;
using OAuth2.Core.Models;

namespace OAuth2.Core.Interfaces
{
    public interface ITokenService
    {
        Task<OAuth2Token> GenerateTokenAsync(OAuth2Client client, OAuth2User user, IEnumerable<OAuth2Scope> scopes, GrantType grantType);
        Task<bool> ValidateTokenAsync(string token);
        Task<OAuth2Token> RefreshTokenAsync(string refreshToken);
        Task RevokeTokenAsync(string token);
    }
}