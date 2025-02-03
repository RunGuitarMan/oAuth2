using System.Threading.Tasks;
using OAuth2.Core.Models;

namespace OAuth2.Core.Interfaces
{
    public interface IClientService
    {
        Task RegisterClientAsync(OAuth2Client client);
        Task<OAuth2Client> ValidateClientAsync(string clientId, string clientSecret);
        Task UpdateClientAsync(OAuth2Client client);
    }
}