using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Server.Services;
using System;

namespace eClassRoom.Server.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.Username) || string.IsNullOrEmpty(loginDto.Password))
                return Forbid();

            var user = await _authService.ValidateUserAsync(loginDto.Username, loginDto.Password);
            if (user != null)
            {
                var (token, expiresAt) = _authService.GenerateJwtTokenWithExpiration(user);
                var result = new LoginResultDto
                {
                    Token = token,
                    ExpiresAt = expiresAt
                };
                return Ok(result);
            }
            return Unauthorized();
        }

        // La génération du token est déléguée au service
    }
}
