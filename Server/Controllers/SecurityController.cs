using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Shared.Dtos;
using Server.Services;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/security")]
    public class SecurityController : ControllerBase
    {
        private readonly SecurityService _securityService;

        public SecurityController(SecurityService securityService)
        {
            _securityService = securityService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResultDto>> SecureLogin([FromBody] LoginDto loginDto)
        {
            var result = await _securityService.SecureLoginAsync(loginDto);
            if (result == null)
                return Unauthorized();
            return Ok(result);
        }

        [HttpGet("login2")]
        public async Task<ActionResult<LoginResultDto>> SecureLogin2([FromQuery] string username, [FromQuery] string password)
        {
            var loginDto = new LoginDto { Username = username, Password = password };
            var result = await _securityService.SecureLoginAsync(loginDto);
            if (result == null)
                return Unauthorized();
            return Ok(result);
        }
    }
}
