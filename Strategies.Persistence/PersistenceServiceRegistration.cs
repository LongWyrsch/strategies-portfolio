using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Strategies.Domain.Persistence;

namespace Strategies.Persistence;

/// <summary>
/// This class is responsible for registering the persistence services to the DI container.
/// </summary>
public static class PersistenceServiceRegistration
{
    // This allows another project to easily register the below services to its DI container and use them.
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<StrategiesContext>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();


        // Get the connection string from appsettings.json. In this case, with SQLite, the connection string is the path to the database file.
        var connectionString = configuration.GetConnectionString("StrategiesDatabase") ?? throw new InvalidOperationException("StrategiesDatabase connection string missing");
        // Get the path to the root of the solution (where the .db file should be located).
        // Instead of using a relative path (e.g. "./"), an abosulte path is referenced in appsettings.json so that different executable (ConsoleApp or Test layer) can find it no matter where the executable is run from (e.g. from Bin/).
        var projectRoot = configuration.GetConnectionString("ProjectRoot") ?? throw new InvalidOperationException("ProjectRoot missing");
        // Combine the path to the root of the solution with the path to the database file (which is also the connection string in case of SQLite)
        var dbPath = Path.Combine(projectRoot, connectionString);
        // Add the DbContext to the container while setting the connection string
        services.AddDbContext<StrategiesContext>(options => options
                                                                .UseSqlite($"Data Source={dbPath}")
                                                                .EnableSensitiveDataLogging()); // Show query parameter values (by default, they're hidden)
        return services;
    }
}