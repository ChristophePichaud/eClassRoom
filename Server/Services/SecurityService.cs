using Shared.Dtos;
using System;
using System.Threading.Tasks;

namespace Server.Services
{
    public class SecurityService
    {
        private readonly AuthService _authService;

        public SecurityService(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginResultDto> SecureLoginAsync(LoginDto loginDto)
        {
            var user = await _authService.ValidateUserAsync(loginDto.Username, loginDto.Password);
            if (user == null)
                return null;

            var (token, expiresAt) = _authService.GenerateJwtTokenWithExpiration(user);
            return new LoginResultDto
            {
                Token = token,
                ExpiresAt = expiresAt
            };
        }
    }
}
