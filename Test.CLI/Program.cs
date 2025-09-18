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
                // Exemple d'ajout d'une machine virtuelle liée à la salle via la relation many-to-many
                var machine = new MachineVirtuelle
                {
                    Name = "VM-Test",
                    TypeOS = "Windows",
                    TypeVM = "Standard",
                    Sku = "Standard_B2s",
                    Offer = "WindowsServer",
                    Version = "2019-Datacenter",
                    DiskISO = "iso/path",
                    NomMarketingVM = "VM Marketing",
                    FichierRDP = "file.rdp",
                    Supervision = "Aucune",
                    StagiaireId = formateur.Id // ou un stagiaire réel
                };
                db.MachinesVirtuelles.Add(machine);
                await db.SaveChangesAsync();

                var salleDto = new SalleDeFormationDto
                {
                    ClientId = client.Id,
                    Client = new ClientDto
                    {
                        Id = client.Id,
                        NomSociete = client.NomSociete,
                        Adresse = client.Adresse,
                        CodePostal = client.CodePostal,
                        Ville = client.Ville,
                        Pays = client.Pays,
                        EmailAdministrateur = client.EmailAdministrateur,
                        Mobile = client.Mobile
                    },
                    Nom = "Salle Test CLI",
                    FormateurId = formateur.Id,
                    Formateur = new UtilisateurDto
                    {
                        Id = formateur.Id,
                        Email = formateur.Email,
                        Nom = formateur.Nom,
                        Prenom = formateur.Prenom,
                        MotDePasse = formateur.MotDePasse,
                        Role = formateur.Role.ToString(),
                        ClientId = formateur.ClientId
                    },
                    DateDebut = DateTime.UtcNow,
                    DateFin = DateTime.UtcNow.AddDays(1),
                    Stagiaires = new List<UtilisateurDto>()
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
                        // Ajout de la relation many-to-many
                        salle.Machines.Add(machine);
                        await dbWithLogging.SaveChangesAsync();
                    }
                }
                Console.WriteLine($"SalleDeFormation créée : {salleDto.Nom}");
            }
        }
    }
}
