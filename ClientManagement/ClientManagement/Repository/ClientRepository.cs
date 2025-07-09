using AutoMapper;
using ClientManagement.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace ClientManagement.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientContext _context;
        private readonly IMapper _mapper;

        public ClientRepository(ClientContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Client>> GetAllClientAsync()
        {
            var clients = await _context.Clients.ToListAsync();
            return clients;
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            return client;
        }

        public async Task<int> AddClientAsync(ClientRequest clientRequest)
        {
            var client = _mapper.Map<Client>(clientRequest);
            client.LicenceKey = Guid.NewGuid();
            client.CreatedDate = DateTime.Now;

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return client.ClientId;
        }

        public async Task<Client> UpdateClientAsync(Client client, ClientRequest clientRequest)
        {
            _mapper.Map(clientRequest, client);

            client.LicenceKey = Guid.NewGuid();
            client.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<bool> UpdateClientPatchAsync(Client client, JsonPatchDocument clientRequest)
        {
            client.UpdatedDate = DateTime.Now;

            clientRequest.ApplyTo(client);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteClientAsync(Client client)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
