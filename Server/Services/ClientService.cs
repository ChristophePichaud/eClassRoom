using EFModel;
using EFModel.Models;
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
            entity.DomainName = dto.DomainName;
            entity.BillingEmail = dto.BillingEmail;
            entity.AddressLine1 = dto.AddressLine1;
            entity.AddressLine2 = dto.AddressLine2;
            entity.CodePostal = dto.CodePostal;
            entity.Ville = dto.Ville;
            entity.Pays = dto.Pays;
            entity.Mobile = dto.Mobile;
            entity.AdminUserId = dto.AdminUserId;
            if (!string.IsNullOrEmpty(dto.LicenseType) && System.Enum.TryParse<LicenseType>(dto.LicenseType, out var licenseType))
            {
                entity.LicenseType = licenseType;
            }
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
            DomainName = c.DomainName,
            BillingEmail = c.BillingEmail,
            AddressLine1 = c.AddressLine1,
            AddressLine2 = c.AddressLine2,
            CodePostal = c.CodePostal,
            Ville = c.Ville,
            Pays = c.Pays,
            Mobile = c.Mobile,
            AdminUserId = c.AdminUserId,
            LicenseType = c.LicenseType.ToString()
        };

        private static Client FromDto(ClientDto dto) => new Client
        {
            Id = dto.Id,
            NomSociete = dto.NomSociete,
            DomainName = dto.DomainName,
            BillingEmail = dto.BillingEmail,
            AddressLine1 = dto.AddressLine1,
            AddressLine2 = dto.AddressLine2,
            CodePostal = dto.CodePostal,
            Ville = dto.Ville,
            Pays = dto.Pays,
            Mobile = dto.Mobile,
            AdminUserId = dto.AdminUserId,
            LicenseType = !string.IsNullOrEmpty(dto.LicenseType) && System.Enum.TryParse<LicenseType>(dto.LicenseType, out var lt) ? lt : LicenseType.Professional
        };
    }
}
