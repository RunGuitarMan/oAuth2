using System.Threading.Tasks;
using OAuth2.Core.Models;

namespace OAuth2.Core.Interfaces
{
    public interface IOAuth2TokenStore
    {
        Task SaveTokenAsync(OAuth2Token token);
        Task<OAuth2Token> GetTokenAsync(string tokenId);
        Task RemoveTokenAsync(string tokenId);
    }
}