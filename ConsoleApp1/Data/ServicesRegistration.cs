using ConsoleApp1.NewFolder;
using ConsoleApp1.Patterns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace ConsoleApp1.Data;

public static class ServicesRegistration
{
    public static void Config(this IServiceCollection services)
    {
        services.AddScoped<DTRManager>();
        services.AddDbContext<AppDbContext>(optionsBuilder =>
        {
            var connString = ConnectionStringProvider.GetConnection();
             //optionsBuilder.UseSqlServer(connString);
            optionsBuilder.UseMySql(connString, ServerVersion.AutoDetect(connString));
        });
    }

    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var connString = ConnectionStringProvider.GetConnection();
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            //optionsBuilder.UseSqlServer(connString);
            optionsBuilder.UseMySql(connString, ServerVersion.AutoDetect(connString));
            return new AppDbContext(optionsBuilder.Options);
        }
    }

    public class ConnectionStringProvider
    {
        public static string GetConnection()
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(AppContext.BaseDirectory)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
            //return "Server=localhost;Database=test;user=root;password=root;";
            return config.GetConnectionString("mysql");
        }
    } 
}
