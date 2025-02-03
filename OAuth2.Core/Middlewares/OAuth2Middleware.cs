using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OAuth2.Core.Helpers;
using OAuth2.Core.Interfaces;

namespace OAuth2.Core.Middlewares
{
    public class OAuth2Middleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService _tokenService;

        public OAuth2Middleware(RequestDelegate next, ITokenService tokenService)
        {
            _next = next;
            _tokenService = tokenService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Извлечение токена из заголовка Authorization
            var authHeader = context.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();
                if (await _tokenService.ValidateTokenAsync(token))
                {
                    // Если токен валиден, можно извлечь данные и установить ClaimsPrincipal в context.User
                    // Здесь можно расширить логику для извлечения claim’ов из токена
                }
                else
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Invalid token");
                    return;
                }
            }

            await _next(context);
        }
    }
}