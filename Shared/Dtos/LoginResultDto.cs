using System;

namespace Shared.Dtos
{
    public class LoginResultDto
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
