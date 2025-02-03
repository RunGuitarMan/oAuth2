using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OAuth2.Core.Configuration;
using OAuth2.Core.Interfaces;
using OAuth2.Core.Services;

namespace OAuth2.Core.Extensions
{
    public static class OAuth2ServiceCollectionExtensions
    {
        public static IServiceCollection AddOAuth2(this IServiceCollection services, IConfiguration configuration)
        {
            // Конфигурация опций из appsettings.json
            services.Configure<OAuth2Options>(options => configuration.GetSection("OAuth2").Bind(options));
            // Регистрация IOAuth2Configuration, оборачивающей опции
            services.AddSingleton<IOAuth2Configuration>(sp =>
            {
                var options = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<OAuth2Options>>().Value;
                return new OAuth2Configuration(options);
            });

            // Регистрация хранилища токенов (выберите InMemory или Database)
            services.AddSingleton<IOAuth2TokenStore, InMemoryTokenStore>();

            // Регистрация сервисов
            services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<IAuthorizationService, AuthorizationService>();
            services.AddSingleton<IClientService, ClientService>();

            return services;
        }
    }
}