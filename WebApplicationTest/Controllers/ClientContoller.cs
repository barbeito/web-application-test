using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTest.Domain.Interfaces.Services;
using WebApplicationTest.Service.Models;

namespace WebApplicationTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientContoller : ControllerBase
    {
        private readonly ILogger<ClientContoller> _logger;
        private readonly IClientService _clientService;

        public ClientContoller(IClientService clientService, ILogger<ClientContoller> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] ClientModel client)
        {
            try
            {
                var result = await _clientService.Insert(client);

                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error on request {nameof(GetAll)} from {nameof(ClientContoller)}. Error: {ex.Message}");
                return Problem(ex.Message, statusCode: 500);
            }
        }
    }
}
