using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EFModel;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Server.Services
{
    public class AuthService
    {
        private readonly EClassRoomDbContext _db;
        private readonly IConfiguration _config;

        public AuthService(EClassRoomDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public async Task<Utilisateur?> ValidateUserAsync(string username, string password)
        {
            // Ici, username = Email
            var user = await _db.Utilisateurs.FirstOrDefaultAsync(u => u.Email == username);
            if (user == null)
                return null;

            // Vérification du hash du mot de passe (ici simplifié, à adapter en prod)
            if (user.MotDePasse != password) // Remplacer par une vraie vérification de hash
                return null;

            return user;
        }

        public string GenerateJwtToken(Utilisateur user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("UserId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtCredentials:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JwtCredentials:Issuer"],
                audience: _config["JwtCredentials:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public (string token, DateTime expiresAt) GenerateJwtTokenWithExpiration(Utilisateur user)
        {
            var expires = DateTime.UtcNow.AddHours(2); // ou la durée configurée
            var token = GenerateJwtToken(user); // Utilise la méthode existante
            return (token, expires);
        }
    }
}