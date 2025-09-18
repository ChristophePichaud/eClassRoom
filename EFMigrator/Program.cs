using System;
using EFModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

class Program
{
    static void Main(string[] args)
    {
        // Charger la config (appsettings.json)
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var services = new ServiceCollection();
        services.AddDbContext<EClassRoomDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        var provider = services.BuildServiceProvider();

        using (var scope = provider.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<EClassRoomDbContext>();
            db.Database.Migrate();
            Console.WriteLine("Migration appliquée !");
        }
    }
}