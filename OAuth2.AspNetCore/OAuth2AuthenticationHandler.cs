using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OAuth2.Core.Interfaces;

namespace OAuth2.AspNetCore
{
    public class OAuth2AuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ITokenService _tokenService;

        public OAuth2AuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ITokenService tokenService)
            : base(options, logger, encoder, clock)
        {
            _tokenService = tokenService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Извлечение токена из заголовка Authorization
            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                return AuthenticateResult.NoResult();
            }

            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            if (!await _tokenService.ValidateTokenAsync(token))
            {
                return AuthenticateResult.Fail("Invalid token");
            }

            // Создаем ClaimsPrincipal; в реальной реализации следует извлечь claim’ы из токена
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "user_id_placeholder") };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}