using Microsoft.EntityFrameworkCore;

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

            // Client - SalleDeFormation
            modelBuilder.Entity<SalleDeFormation>()
                .HasOne(s => s.Client)
                .WithMany(c => c.Salles)
                .HasForeignKey(s => s.ClientId);

            // Client - Facture
            modelBuilder.Entity<Facture>()
                .HasOne(f => f.Client)
                .WithMany()
                .HasForeignKey(f => f.ClientId);

            // SalleDeFormation - MachineVirtuelle
            modelBuilder.Entity<MachineVirtuelle>()
                .HasOne(m => m.Salle)
                .WithMany(s => s.Machines)
                .HasForeignKey(m => m.SalleDeFormationId);

            // SalleDeFormation - Stagiaires (many-to-many possible, à adapter si navigation)

            // MachineVirtuelle - Stagiaire
            modelBuilder.Entity<MachineVirtuelle>()
                .HasOne(m => m.Stagiaire)
                .WithMany()
                .HasForeignKey(m => m.StagiaireId);
        }
    }
}