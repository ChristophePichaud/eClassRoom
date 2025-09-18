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

namespace Test.CLI
{
    public class Options
    {
        [Option('a', "admin", Required = false, HelpText = "Create a sample admin user in a sample customer.")]
        public bool CreateAdmin { get; set; }

        [Option('b', "virtualroom", Required = false, HelpText = "Create a sample VirtualRoom.")]
        public bool CreateVirtualRoom { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptions);
        }

        static void RunOptions(Options opts)
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

            if (opts.CreateAdmin)
            {
                // Create a sample client
                var client = new Client
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
                db.Clients.Add(client);
                db.SaveChanges();

                // Create a sample admin user
                var admin = new Utilisateur
                {
                    Email = "admin@samplecorp.com",
                    Nom = "Admin",
                    Prenom = "Super",
                    MotDePasse = "admin123",
                    Role = RoleUtilisateur.Administrateur,
                    ClientId = client.Id
                };
                db.Utilisateurs.Add(admin);
                db.SaveChanges();

                Console.WriteLine($"Sample admin user and customer created. ClientId={client.Id}, AdminId={admin.Id}");
            }

            if (opts.CreateVirtualRoom)
            {
                // Find a client and a formateur (admin)
                var client = db.Clients.FirstOrDefault();
                var formateur = db.Utilisateurs.FirstOrDefault(u => u.Role == RoleUtilisateur.Administrateur);
                if (client == null || formateur == null)
                {
                    Console.WriteLine("No client or admin user found. Run with -a first.");
                    return;
                }

                // Create a sample stagiaire
                var stagiaire = new Utilisateur
                {
                    Email = "stagiaire@samplecorp.com",
                    Nom = "Stagiaire",
                    Prenom = "Test",
                    MotDePasse = "stagiaire123",
                    Role = RoleUtilisateur.Stagiaire,
                    ClientId = client.Id
                };
                db.Utilisateurs.Add(stagiaire);
                db.SaveChanges();

                // Create a virtual room DTO
                var salleDto = new SalleDeFormationDto
                {
                    Nom = "Formation PostgreSQL",
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
                    DateDebut = DateTime.Now,
                    DateFin = DateTime.Now.AddDays(3),
                    Stagiaires = new List<UtilisateurDto>
                    {
                        new UtilisateurDto
                        {
                            Id = stagiaire.Id,
                            Email = stagiaire.Email,
                            Nom = stagiaire.Nom,
                            Prenom = stagiaire.Prenom,
                            MotDePasse = stagiaire.MotDePasse,
                            Role = stagiaire.Role.ToString(),
                            ClientId = stagiaire.ClientId
                        }
                    }
                };

                // Use the service to add the room
                var salleService = new SalleDeFormationService(db);
                salleService.AddAsync(salleDto).Wait();

                Console.WriteLine($"Sample virtual room created. Nom={salleDto.Nom}");
            }
        }
    }
}
