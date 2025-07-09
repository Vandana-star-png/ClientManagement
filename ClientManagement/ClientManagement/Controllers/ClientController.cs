using ClientManagement.Models;
using ClientManagement.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ClientManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var clients = await _clientRepository.GetAllClientAsync();

            if(clients == null || !clients.Any())
            {
                return NotFound("No Clients found");
            }
            return Ok(clients);
        }

        [HttpGet("{id}", Name = "GetClientById")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Client ID must be positive number");
            }

            var client = await _clientRepository.GetClientByIdAsync(id);

            if (client == null)
            {
                return NotFound($"Client with ID {id} not found");
            }

            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] ClientRequest clientRequest)
        {
            if (clientRequest == null)
            {
                return BadRequest("Client data cannot be null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = await _clientRepository.AddClientAsync(clientRequest);

            var client = await _clientRepository.GetClientByIdAsync(id);

            return CreatedAtRoute("GetClientById", new { id = id }, client);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ClientRequest clientRequest)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest("Client data is required");
            }

            var existingClient = await _clientRepository.GetClientByIdAsync(id);

            if (existingClient == null)
            {
                return BadRequest($"Client with ID {id} not found");
            }

            var updatedClient = await _clientRepository.UpdateClientAsync(existingClient, clientRequest);

            return Ok(updatedClient);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePatchAsync([FromBody] JsonPatchDocument clientRequest, [FromRoute] int id)
        {
            if (clientRequest == null)
            {
                return BadRequest("Patch document is required");
            }

            var existingClient = await _clientRepository.GetClientByIdAsync(id);
            if (existingClient == null)
            {
                return NotFound($"Client with ID {id} not found");
            }

            var isUpdated = await _clientRepository.UpdateClientPatchAsync(existingClient, clientRequest);
            if (!isUpdated)
            {
                return StatusCode(500, "An error occurred while saving the updated client data");
            }
            return Ok("Client data updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var existingClient = await _clientRepository.GetClientByIdAsync(id);
            if (existingClient == null)
            {
                return BadRequest($"Client with ID {id} not found");
            }

            var isDeleted = await _clientRepository.DeleteClientAsync(existingClient);
            if (!isDeleted)
            {
                return BadRequest($"Failed to delete client with ID {id}");
            }
            return Ok($"Client with ID {id} deleted successfully");
        }
    }
}
