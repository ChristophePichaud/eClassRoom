using Microsoft.AspNetCore.Mvc;
using Server.Services;
using Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _service;

        public ClientController(ClientService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClientDto>>> GetAll()
        {
            var clients = await _service.GetAllAsync();
            return Ok(clients);
        }
    }
}