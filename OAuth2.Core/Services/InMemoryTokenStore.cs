using System.Threading.Tasks;
using System.Collections.Concurrent;
using OAuth2.Core.Interfaces;
using OAuth2.Core.Models;

namespace OAuth2.Core.Services
{
    public class InMemoryTokenStore : IOAuth2TokenStore
    {
        // Хранилище токенов в памяти
        private readonly ConcurrentDictionary<string, OAuth2Token> _tokens = new ConcurrentDictionary<string, OAuth2Token>();

        public async Task SaveTokenAsync(OAuth2Token token)
        {
            _tokens[token.TokenId] = token;
            await Task.CompletedTask;
        }

        public async Task<OAuth2Token> GetTokenAsync(string tokenId)
        {
            _tokens.TryGetValue(tokenId, out var token);
            return await Task.FromResult(token);
        }

        public async Task RemoveTokenAsync(string tokenId)
        {
            _tokens.TryRemove(tokenId, out var _);
            await Task.CompletedTask;
        }
    }
}