using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Server.Services;

namespace eClassRoom.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var user = await _authService.ValidateUserAsync(login.Username, login.Password);
            if (user != null)
            {
                var token = _authService.GenerateJwtToken(user);
                return Ok(new { token });
            }
            return Unauthorized();
        }

        // La génération du token est déléguée au service
    }

    // LoginDto est dans Shared.Dtos
}
