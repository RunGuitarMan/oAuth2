using System;
using System.Threading.Tasks;
using OAuth2.Core.Interfaces;
using OAuth2.Core.Models;

namespace OAuth2.Core.Services
{
    public class ClientService : IClientService
    {
        // Пример in-memory хранилища клиентов
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<string, OAuth2Client> clients = new System.Collections.Concurrent.ConcurrentDictionary<string, OAuth2Client>();

        public async Task RegisterClientAsync(OAuth2Client client)
        {
            if (!clients.TryAdd(client.ClientId, client))
            {
                throw new Exception("Client already exists");
            }
            await Task.CompletedTask;
        }

        public async Task<OAuth2Client> ValidateClientAsync(string clientId, string clientSecret)
        {
            if (clients.TryGetValue(clientId, out var client))
            {
                if (client.ClientSecret == clientSecret)
                    return await Task.FromResult(client);
            }
            return await Task.FromResult<OAuth2Client>(null);
        }

        public async Task UpdateClientAsync(OAuth2Client client)
        {
            clients[client.ClientId] = client;
            await Task.CompletedTask;
        }
    }
}