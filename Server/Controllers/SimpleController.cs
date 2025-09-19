using Microsoft.AspNetCore.Mvc;
using Server.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace eClassRoom.Server.Controllers
{
    [ApiController]
    [Route("api/simple")]
    //[Authorize]
    public class SimpleController : ControllerBase
    {
        private readonly SimpleService _service;

        public SimpleController(SimpleService service)
        {
            _service = service;
        }

        [HttpGet("queries")]
        public async Task<ActionResult<List<string>>> GetAllQueries()
        {
            // Retrieve the Bearer token from the Authorization header
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            var results = await _service.GetAllQueriesAsync(token);
            return Ok(results);
        }
    }
}