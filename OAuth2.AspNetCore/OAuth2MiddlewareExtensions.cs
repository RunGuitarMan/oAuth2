using Microsoft.AspNetCore.Builder;
using OAuth2.Core.Middlewares;

namespace OAuth2.AspNetCore
{
    public static class OAuth2MiddlewareExtensions
    {
        public static IApplicationBuilder UseOAuth2(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<OAuth2Middleware>();
        }
    }
}