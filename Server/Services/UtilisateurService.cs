using EFModel;
using EFModel.Models;
using Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Server.Services
{
    public static class UserRoleHelper
    {
        public static UserRole FromStringToUserRole(string value)
        {
            return Enum.TryParse<UserRole>(value, out var result) ? result : UserRole.Student;
        }

        public static string ToStringRole(UserRole role)
        {
            return role.ToString();
        }
    }

    public class UtilisateurService
    {
        private readonly EClassRoomDbContext _db;

        public UtilisateurService(EClassRoomDbContext db)
        {
            _db = db;
        }

        public async Task<List<UtilisateurDto>> GetAllAsync()
        {
            return await _db.Utilisateurs
                .Select(u => new UtilisateurDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    Nom = u.Nom,
                    Prenom = u.Prenom,
                    MotDePasse = u.MotDePasse,
                    Role = UserRoleHelper.ToStringRole(u.Role),
                    ClientId = u.ClientId
                })
                .ToListAsync();
        }

        public async Task<UtilisateurDto> GetByIdAsync(int id)
        {
            var u = await _db.Utilisateurs.FindAsync(id);
            if (u == null) return null;
            return new UtilisateurDto
            {
                Id = u.Id,
                Email = u.Email,
                Nom = u.Nom,
                Prenom = u.Prenom,
                MotDePasse = u.MotDePasse,
                Role = UserRoleHelper.ToStringRole(u.Role),
                ClientId = u.ClientId
            };
        }

        public async Task<UtilisateurDto> AddAsync(UtilisateurDto dto)
        {
            var u = new Utilisateur
            {
                Email = dto.Email,
                Nom = dto.Nom,
                Prenom = dto.Prenom,
                MotDePasse = dto.MotDePasse,
                Role = UserRoleHelper.FromStringToUserRole(dto.Role),
                ClientId = dto.ClientId
            };
            _db.Utilisateurs.Add(u);
            await _db.SaveChangesAsync();
            // Retourne le DTO créé avec l'ID mis à jour
            return new UtilisateurDto
            {
                Id = u.Id,
                Email = u.Email,
                Nom = u.Nom,
                Prenom = u.Prenom,
                MotDePasse = u.MotDePasse,
                Role = UserRoleHelper.ToStringRole(u.Role),
                ClientId = u.ClientId
            };
        }

        public async Task<UtilisateurDto> UpdateAsync(int id, UtilisateurDto dto)
        {
            var u = await _db.Utilisateurs.FindAsync(id);
            if (u == null) return null;
            u.Email = dto.Email;
            u.Nom = dto.Nom;
            u.Prenom = dto.Prenom;
            u.MotDePasse = dto.MotDePasse;
            u.Role = UserRoleHelper.FromStringToUserRole(dto.Role);
            u.ClientId = dto.ClientId;
            await _db.SaveChangesAsync();
            return new UtilisateurDto
            {
                Id = u.Id,
                Email = u.Email,
                Nom = u.Nom,
                Prenom = u.Prenom,
                MotDePasse = u.MotDePasse,
                Role = UserRoleHelper.ToStringRole(u.Role),
                ClientId = u.ClientId
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var u = await _db.Utilisateurs.FindAsync(id);
            if (u == null) return false;
            _db.Utilisateurs.Remove(u);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
