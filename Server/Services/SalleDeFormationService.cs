using EFModel;
using Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Server.Services
{
    public class SalleDeFormationService
    {
        private readonly EClassRoomDbContext _db;

        public SalleDeFormationService(EClassRoomDbContext db)
        {
            _db = db;
        }

        public async Task<List<SalleDeFormationDto>> GetAllAsync()
        {
            return await _db.SallesDeFormation
                .Include(s => s.Stagiaires)
                .Include(s => s.Formateur)
                .Select(s => new SalleDeFormationDto
                {
                    Id = s.Id,
                    Nom = s.Nom,
                    ClientId = s.ClientId,
                    Client = s.Client != null ? new ClientDto
                    {
                        Id = s.Client.Id,
                        NomSociete = s.Client.NomSociete,
                        Adresse = s.Client.Adresse,
                        CodePostal = s.Client.CodePostal,
                        Ville = s.Client.Ville,
                        Pays = s.Client.Pays,
                        EmailAdministrateur = s.Client.EmailAdministrateur,
                        Mobile = s.Client.Mobile
                    } : null,
                    FormateurId = s.FormateurId,
                    Formateur = s.Formateur != null ? new UtilisateurDto
                    {
                        Id = s.Formateur.Id,
                        Email = s.Formateur.Email,
                        Nom = s.Formateur.Nom,
                        Prenom = s.Formateur.Prenom,
                        MotDePasse = s.Formateur.MotDePasse,
                        Role = s.Formateur.Role.ToString(),
                        ClientId = s.Formateur.ClientId
                    } : null,
                    DateDebut = s.DateDebut,
                    DateFin = s.DateFin,
                    Stagiaires = s.Stagiaires.Select(u => new UtilisateurDto
                    {
                        Id = u.Id,
                        Email = u.Email,
                        Nom = u.Nom,
                        Prenom = u.Prenom,
                        MotDePasse = u.MotDePasse,
                        Role = u.Role.ToString(),
                        ClientId = u.ClientId
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<SalleDeFormationDto> GetByIdAsync(int id)
        {
            var s = await _db.SallesDeFormation
                .Include(sf => sf.Stagiaires)
                .Include(sf => sf.Formateur)
                .FirstOrDefaultAsync(sf => sf.Id == id);
            if (s == null) return null;
            return new SalleDeFormationDto
            {
                Id = s.Id,
                Nom = s.Nom,
                ClientId = s.ClientId,
                Client = s.Client != null ? new ClientDto
                {
                    Id = s.Client.Id,
                    NomSociete = s.Client.NomSociete,
                    Adresse = s.Client.Adresse,
                    CodePostal = s.Client.CodePostal,
                    Ville = s.Client.Ville,
                    Pays = s.Client.Pays,
                    EmailAdministrateur = s.Client.EmailAdministrateur,
                    Mobile = s.Client.Mobile
                } : null,
                FormateurId = s.FormateurId,
                Formateur = s.Formateur != null ? new UtilisateurDto
                {
                    Id = s.Formateur.Id,
                    Email = s.Formateur.Email,
                    Nom = s.Formateur.Nom,
                    Prenom = s.Formateur.Prenom,
                    MotDePasse = s.Formateur.MotDePasse,
                    Role = s.Formateur.Role.ToString(),
                    ClientId = s.Formateur.ClientId
                } : null,
                DateDebut = s.DateDebut,
                DateFin = s.DateFin,
                Stagiaires = s.Stagiaires.Select(u => new UtilisateurDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    Nom = u.Nom,
                    Prenom = u.Prenom,
                    MotDePasse = u.MotDePasse,
                    Role = u.Role.ToString(),
                    ClientId = u.ClientId
                }).ToList()
            };
        }

        public async Task AddAsync(SalleDeFormationDto dto)
        {
            Utilisateur formateur = null;
            if (dto.Formateur != null)
            {
                formateur = await _db.Utilisateurs.FindAsync(dto.Formateur.Id);
            }
            var s = new SalleDeFormation
            {
                Nom = dto.Nom,
                Formateur = formateur,
                DateDebut = dto.DateDebut,
                DateFin = dto.DateFin
            };
            _db.SallesDeFormation.Add(s);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, SalleDeFormationDto dto)
        {
            var s = await _db.SallesDeFormation.FindAsync(id);
            if (s == null) return;
            s.Nom = dto.Nom;
            if (dto.Formateur != null)
            {
                var formateur = await _db.Utilisateurs.FindAsync(dto.Formateur.Id);
                s.Formateur = formateur;
            }
            s.DateDebut = dto.DateDebut;
            s.DateFin = dto.DateFin;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var s = await _db.SallesDeFormation.FindAsync(id);
            if (s == null) return;
            _db.SallesDeFormation.Remove(s);
            await _db.SaveChangesAsync();
        }
    }
}
