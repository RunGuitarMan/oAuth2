using System.Threading.Tasks;
using OAuth2.Core.Interfaces;
using OAuth2.Core.Models;

namespace OAuth2.Core.Services
{
    public class DatabaseTokenStore : IOAuth2TokenStore
    {
        // Здесь должна быть реализация с использованием БД (например, через EF Core)
        public async Task SaveTokenAsync(OAuth2Token token)
        {
            // Сохранение токена в БД
            await Task.CompletedTask;
        }

        public async Task<OAuth2Token> GetTokenAsync(string tokenId)
        {
            // Получение токена из БД
            return await Task.FromResult<OAuth2Token>(null);
        }

        public async Task RemoveTokenAsync(string tokenId)
        {
            // Удаление токена из БД
            await Task.CompletedTask;
        }
    }
}