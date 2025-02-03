using System.Threading.Tasks;
using OAuth2.Core.Models;

namespace OAuth2.Core.Interfaces
{
    public interface IAuthorizationService
    {
        Task<bool> AuthorizeAsync(OAuth2User user, string scope);
        bool HasPermission(OAuth2User user, OAuth2Scope scope);
    }
}