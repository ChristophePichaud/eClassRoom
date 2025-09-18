using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EFModel
{
    public class EClassRoomDbContextFactory : IDesignTimeDbContextFactory<EClassRoomDbContext>
    {
        public EClassRoomDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EClassRoomDbContext>();
            // Mets ici ta chaîne de connexion PostgreSQL
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=eclassroom;Username=postgres;Password=admin");

            return new EClassRoomDbContext(optionsBuilder.Options);
        }
    }
}