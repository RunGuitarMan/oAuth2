using System.Threading.Tasks;
using OAuth2.Core.Interfaces;
using OAuth2.Core.Models;

namespace OAuth2.Core.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        public async Task<bool> AuthorizeAsync(OAuth2User user, string scope)
        {
            // Здесь можно реализовать проверку прав доступа пользователя к scope
            return await Task.FromResult(true);
        }

        public bool HasPermission(OAuth2User user, OAuth2Scope scope)
        {
            // Пример проверки: всегда возвращаем true (замените на реальную логику)
            return true;
        }
    }
}