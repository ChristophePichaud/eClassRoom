using EFModel;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public class ClientService
    {
        private readonly EClassRoomDbContext _db;

        public ClientService(EClassRoomDbContext db)
        {
            _db = db;
        }

        public async Task<List<ClientDto>> GetAllAsync()
        {
            var clients = await _db.Clients.ToListAsync();
            return clients.Select(ToDto).ToList();
        }

        public async Task<ClientDto> GetByIdAsync(int id)
        {
            var client = await _db.Clients.FindAsync(id);
            return client == null ? null : ToDto(client);
        }

        public async Task<ClientDto> AddAsync(ClientDto dto)
        {
            var entity = FromDto(dto);
            _db.Clients.Add(entity);
            await _db.SaveChangesAsync();
            return ToDto(entity);
        }

        public async Task<bool> UpdateAsync(int id, ClientDto dto)
        {
            var entity = await _db.Clients.FindAsync(id);
            if (entity == null) return false;
            entity.NomSociete = dto.NomSociete;
            entity.Adresse = dto.Adresse;
            entity.CodePostal = dto.CodePostal;
            entity.Ville = dto.Ville;
            entity.Pays = dto.Pays;
            entity.EmailAdministrateur = dto.EmailAdministrateur;
            entity.Mobile = dto.Mobile;
            // MotDePasseAdministrateur: à ne pas exposer ni modifier ici pour la sécurité
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _db.Clients.FindAsync(id);
            if (entity == null) return false;
            _db.Clients.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        private static ClientDto ToDto(Client c) => new ClientDto
        {
            Id = c.Id,
            NomSociete = c.NomSociete,
            Adresse = c.Adresse,
            CodePostal = c.CodePostal,
            Ville = c.Ville,
            Pays = c.Pays,
            EmailAdministrateur = c.EmailAdministrateur,
            Mobile = c.Mobile
            // Ne jamais exposer MotDePasseAdministrateur
        };

        private static Client FromDto(ClientDto dto) => new Client
        {
            Id = dto.Id,
            NomSociete = dto.NomSociete,
            Adresse = dto.Adresse,
            CodePostal = dto.CodePostal,
            Ville = dto.Ville,
            Pays = dto.Pays,
            EmailAdministrateur = dto.EmailAdministrateur,
            Mobile = dto.Mobile
            // MotDePasseAdministrateur à gérer lors de la création initiale uniquement
        };
    }
}
