using ClientManagement.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace ClientManagement.Repository
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAllClientAsync();

        Task<Client> GetClientByIdAsync(int id);

        Task<int> AddClientAsync(ClientRequest clientRequest);

        Task<Client> UpdateClientAsync(Client client, ClientRequest clientRequest);

        Task<bool> UpdateClientPatchAsync(Client client, JsonPatchDocument clientRequest);

        Task<bool> DeleteClientAsync(Client client);
    }
}