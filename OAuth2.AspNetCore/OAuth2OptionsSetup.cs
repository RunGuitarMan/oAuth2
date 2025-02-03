using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace OAuth2.AspNetCore
{
    public class OAuth2OptionsSetup : IConfigureOptions<AuthenticationSchemeOptions>
    {
        public void Configure(AuthenticationSchemeOptions options)
        {
            // Здесь можно установить опции по умолчанию, если требуется
        }
    }
}