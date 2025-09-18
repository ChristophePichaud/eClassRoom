using Microsoft.EntityFrameworkCore;
using EFModel;

namespace EFModel
{
    public class EClassRoomDbContext : DbContext
    {
        public EClassRoomDbContext(DbContextOptions<EClassRoomDbContext> options)
            : base(options)
        {
        }

        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<SalleDeFormation> SallesDeFormation { get; set; }
        public DbSet<ProvisionningVM> ProvisionningVMs { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<MachineVirtuelle> MachinesVirtuelles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ProvisionningVM
            modelBuilder.Entity<ProvisionningVM>()
                .HasOne(p => p.SalleDeFormation)
                .WithMany()
                .HasForeignKey(p => p.SalleDeFormationId);

            modelBuilder.Entity<ProvisionningVM>()
                .HasOne(p => p.Stagiaire)
                .WithMany()
                .HasForeignKey(p => p.StagiaireId);

            // Client - Utilisateur
            modelBuilder.Entity<Utilisateur>()
                .HasOne(u => u.Client)
                .WithMany(c => c.Utilisateurs)
                .HasForeignKey(u => u.ClientId);

            // Client - SalleDeFormation (relation explicite avec navigation inverse)
            modelBuilder.Entity<SalleDeFormation>()
                .HasOne(s => s.Client)
                .WithMany(c => c.SallesDeFormation)
                .HasForeignKey(s => s.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Client - Facture
            modelBuilder.Entity<Facture>()
                .HasOne(f => f.Client)
                .WithMany()
                .HasForeignKey(f => f.ClientId);

            // SalleDeFormation - Stagiaires (many-to-many possible, à adapter si navigation)

            // MachineVirtuelle - Stagiaire
            modelBuilder.Entity<MachineVirtuelle>()
                .HasOne(m => m.Stagiaire)
                .WithMany()
                .HasForeignKey(m => m.StagiaireId);

            // Many-to-many SalleDeFormation <-> MachineVirtuelle
            modelBuilder.Entity<SalleDeFormation>()
                .HasMany(s => s.Machines)
                .WithMany(m => m.Salles)
                .UsingEntity<Dictionary<string, object>>(
                    "SalleDeFormationMachineVirtuelle",
                    j => j
                        .HasOne<MachineVirtuelle>()
                        .WithMany()
                        .HasForeignKey("MachineVirtuelleId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<SalleDeFormation>()
                        .WithMany()
                        .HasForeignKey("SalleDeFormationId")
                        .OnDelete(DeleteBehavior.Cascade)
                );

            // SalleDeFormation - Formateur (Utilisateur)
            modelBuilder.Entity<SalleDeFormation>()
                .HasOne(s => s.Formateur)
                .WithMany()
                .HasForeignKey(s => s.FormateurId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}