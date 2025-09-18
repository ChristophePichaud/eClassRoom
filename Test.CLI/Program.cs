using System;
using System.Collections.Generic;
using System.IO;
using CommandLine;
using EFModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Server.Services;
using Shared.Dtos;
using System.Linq;
using System.Threading.Tasks;

namespace Test.CLI
{
    public class Options
    {
        [Option('a', "admin", Required = false, HelpText = "Create a sample admin user in a sample customer.")]
        public bool CreateAdmin { get; set; }

        [Option('b', "virtualroom", Required = false, HelpText = "Create a sample VirtualRoom.")]
        public bool CreateVirtualRoom { get; set; }

        [Option('c', "createsalle", Required = false, HelpText = "Create a SalleDeFormation only.")]
        public bool CreateSalle { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(opts => RunOptionsAsync(opts).Wait());
        }

        static async Task RunOptionsAsync(Options opts)
        {
            // Load configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<EClassRoomDbContext>()
                .UseNpgsql(connectionString)
                .Options;

            using var db = new EClassRoomDbContext(options);

            var clientService = new ClientService(db);
            var utilisateurService = new UtilisateurService(db);
            var salleService = new SalleDeFormationService(db);

            if (opts.CreateAdmin)
            {
                // Crée un client via DTO/service
                var clientDto = new ClientDto
                {
                    NomSociete = "SampleCorp",
                    Adresse = "123 Rue de Test",
                    CodePostal = "75000",
                    Ville = "Paris",
                    Pays = "France",
                    EmailAdministrateur = "admin@samplecorp.com",
                    Mobile = "0600000000",
                    MotDePasseAdministrateur = "admin123"
                };
                var createdClient = await clientService.AddAsync(clientDto);

                // Crée un utilisateur admin via DTO/service
                var adminDto = new UtilisateurDto
                {
                    Email = "admin@samplecorp.com",
                    Nom = "Admin",
                    Prenom = "Super",
                    MotDePasse = "admin123",
                    Role = RoleUtilisateur.Administrateur.ToString(),
                    ClientId = createdClient.Id
                };
                var createdAdmin = await utilisateurService.AddAsync(adminDto);

                Console.WriteLine($"Sample admin user and customer created. ClientId={createdClient.Id}, AdminId={createdAdmin.Id}");
            }

            if (opts.CreateVirtualRoom)
            {
                // Récupère un client et un admin via service
                var clientsList = await clientService.GetAllAsync();
                var client = clientsList.FirstOrDefault();
                var utilisateursList = await utilisateurService.GetAllAsync();
                var formateur = utilisateursList.FirstOrDefault(u => u.Role == RoleUtilisateur.Administrateur.ToString());
                if (client == null || formateur == null)
                {
                    Console.WriteLine("No client or admin user found. Run with -a first.");
                    return;
                }

                // Crée un stagiaire via DTO/service
                var stagiaireDto = new UtilisateurDto
                {
                    Email = "stagiaire@samplecorp.com",
                    Nom = "Stagiaire",
                    Prenom = "Test",
                    MotDePasse = "stagiaire123",
                    Role = RoleUtilisateur.Stagiaire.ToString(),
                    ClientId = client.Id
                };
                var createdStagiaire = await utilisateurService.AddAsync(stagiaireDto);

                // Crée une salle de formation via DTO/service
                var salleDto = new SalleDeFormationDto
                {
                    ClientId = client.Id,
                    Client = client, // Ajout du client dans le DTO
                    Nom = "Formation PostgreSQL",
                    FormateurId = formateur.Id,
                    Formateur = formateur,
                    DateDebut = DateTime.UtcNow,
                    DateFin = DateTime.UtcNow.AddDays(3),
                    Stagiaires = new List<UtilisateurDto> { createdStagiaire }
                };

                await salleService.AddAsync(salleDto);

                Console.WriteLine($"Sample virtual room created. Nom={salleDto.Nom}");
            }

            if (opts.CreateSalle)
            {
                var client = await db.Clients.FirstOrDefaultAsync();
                var formateur = await db.Utilisateurs.FirstOrDefaultAsync(u => u.Role == RoleUtilisateur.Administrateur);
                if (client == null || formateur == null)
                {
                    Console.WriteLine("No client or admin user found. Run with -a first.");
                    return;
                }
                // Récupère la première machine virtuelle existante
                var machine = await db.MachinesVirtuelles.FirstOrDefaultAsync();
                if (machine == null)
                {
                    Console.WriteLine("No MachineVirtuelle found in database.");
                    return;
                }
                var machineDto = new MachineVirtuelleDto
                {
                    Id = machine.Id,
                    Name = machine.Name,
                    TypeOS = machine.TypeOS,
                    TypeVM = machine.TypeVM,
                    Sku = machine.Sku,
                    Offer = machine.Offer,
                    Version = machine.Version,
                    DiskISO = machine.DiskISO,
                    NomMarketing = machine.NomMarketing,
                    FichierRDP = machine.FichierRDP,
                    Supervision = machine.Supervision,
                    StagiaireId = machine.StagiaireId
                };

                // Création d'un stagiaire
                var stagiaire = new UtilisateurDto
                {
                    Email = "stagiaire@samplecorp.com",
                    Nom = "Stagiaire",
                    Prenom = "Test",
                    MotDePasse = "stagiaire123",
                    Role = RoleUtilisateur.Stagiaire.ToString(),
                    ClientId = client.Id
                };

                var salleDto = new SalleDeFormationDto
                {
                    ClientId = client.Id,
                    // Ne pas remplir Client ici (pour insert, seul ClientId est utilisé)
                    Nom = "Salle Test CLI",
                    FormateurId = formateur.Id,
                    // Ne pas remplir Formateur ici (pour insert, seul FormateurId est utilisé)
                    DateDebut = DateTime.UtcNow,
                    DateFin = DateTime.UtcNow.AddDays(1),
                    Stagiaires = new List<UtilisateurDto> { stagiaire },
                    Machines = new List<MachineVirtuelleDto> { machineDto }
                };
                // Ajout via service (la gestion de l'association many-to-many doit être gérée dans le service)
                var optionsWithLogging = new DbContextOptionsBuilder<EClassRoomDbContext>()
                    .UseNpgsql(connectionString)
                    .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                    .Options;
                using (var dbWithLogging = new EClassRoomDbContext(optionsWithLogging))
                {
                    var salleServiceWithLogging = new SalleDeFormationService(dbWithLogging);
                    // Ajout de la salle
                    await salleServiceWithLogging.AddAsync(salleDto);
                    // Récupération de la salle créée
                    var salle = await dbWithLogging.SallesDeFormation.OrderByDescending(s => s.Id).FirstOrDefaultAsync();
                    if (salle != null)
                    {
                        // Ajout de la relation many-to-many si besoin (optionnel ici)
                        await dbWithLogging.SaveChangesAsync();
                    }
                }
                Console.WriteLine($"SalleDeFormation créée : {salleDto.Nom}");
            }
        }
    }
}
