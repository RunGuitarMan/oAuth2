using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using OAuth2.Core.Interfaces;
using OAuth2.Core.Models;
using OAuth2.Core.Helpers;

namespace OAuth2.Core.Services
{
    public class TokenService : ITokenService
    {
        private readonly IOAuth2TokenStore _tokenStore;
        private readonly IOAuth2Configuration _configuration;

        public TokenService(IOAuth2TokenStore tokenStore, IOAuth2Configuration configuration)
        {
            _tokenStore = tokenStore;
            _configuration = configuration;
        }

        public async Task<OAuth2Token> GenerateTokenAsync(OAuth2Client client, OAuth2User user, IEnumerable<OAuth2Scope> scopes, GrantType grantType)
        {
            // Генерируем уникальный идентификатор токена
            var tokenId = Guid.NewGuid().ToString();
            var now = DateTime.UtcNow;

            // Создаем обработчик JWT и описание токена
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.SigningKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("sub", user.UserId),
                    new Claim("client_id", client.ClientId),
                    new Claim("token_id", tokenId)
                    // Добавьте дополнительные claim’ы по необходимости
                }),
                Expires = now.AddSeconds(_configuration.AccessTokenLifetime),
                Issuer = _configuration.Issuer,
                Audience = _configuration.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            // Генерация refresh token (можно улучшить алгоритм генерации)
            var refreshToken = Guid.NewGuid().ToString();

            var token = new OAuth2Token
            {
                TokenId = tokenId,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = tokenDescriptor.Expires.Value,
                Scopes = scopes,
                ClientId = client.ClientId,
                UserId = user.UserId
            };

            await _tokenStore.SaveTokenAsync(token);
            return token;
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            try
            {
                // Валидация токена с помощью вспомогательного класса
                var principal = TokenValidator.ValidateToken(token, _configuration.SigningKey, _configuration.Issuer, _configuration.Audience);
                return principal != null;
            }
            catch
            {
                return false;
            }
        }

        public async Task<OAuth2Token> RefreshTokenAsync(string refreshToken)
        {
            // Пример простой реализации: получить токен по refreshToken (в реальном проекте следует реализовать поиск по refreshToken)
            OAuth2Token existingToken = await _tokenStore.GetTokenAsync(refreshToken);
            if(existingToken == null || existingToken.RefreshToken != refreshToken)
                throw new Exception("Invalid refresh token");

            // Обновление access token
            existingToken.ExpiresAt = DateTime.UtcNow.AddSeconds(_configuration.AccessTokenLifetime);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.SigningKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("sub", existingToken.UserId),
                    new Claim("client_id", existingToken.ClientId),
                    new Claim("token_id", existingToken.TokenId)
                }),
                Expires = existingToken.ExpiresAt,
                Issuer = _configuration.Issuer,
                Audience = _configuration.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            existingToken.AccessToken = tokenHandler.WriteToken(securityToken);

            await _tokenStore.SaveTokenAsync(existingToken);
            return existingToken;
        }

        public async Task RevokeTokenAsync(string token)
        {
            await _tokenStore.RemoveTokenAsync(token);
        }
    }
}