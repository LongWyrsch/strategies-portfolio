using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Strategies.Domain;

/// <summary>
/// This class is responsible for registering the domain services to the DI container.
/// </summary>
public static class DomainServiceRegistration
{
    // This allows another project to easily register the below services to its DI container and use them.
    public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Retrieve Cryptomator path from appsettings.json
        var cryptomatorSettings = configuration.GetSection("Cryptomator").Get<Cryptomator>();
        if (cryptomatorSettings == null)
            throw new Exception("Cryptomator settings not found in appsettings.json");
        services.AddSingleton(cryptomatorSettings);

        // Retrieve directories from appsettings.json
        var directories = configuration.GetSection("Directories").Get<Directories>();
        if (directories == null)
            throw new Exception("Directories settings not found.");
        services.AddSingleton(directories);

        // Retrieve Supabase configuration from appsettings.json
        var supabaseConfig = configuration.GetSection("Supabase").Get<SupabaseConfig>();
        if (supabaseConfig == null || string.IsNullOrEmpty(supabaseConfig.Url) || string.IsNullOrEmpty(supabaseConfig.Key))
            throw new Exception("Supabase configuration not found in appsettings.json");

        var options = new Supabase.SupabaseOptions
        {
            AutoConnectRealtime = true,
            AutoRefreshToken = true
        };

        var supabaseClient = new Supabase.Client(supabaseConfig.Url, supabaseConfig.Key, options);
        supabaseClient.InitializeAsync().GetAwaiter().GetResult(); // Ensure initialization is complete

        // Register Supabase client as a singleton
        services.AddSingleton(supabaseClient);

        // Register other services
        services.AddTransient<IFileHandler, FileHandler>();
        services.AddTransient<DatabaseService>();
        services.AddTransient<CsvChartDataReader>();
        services.AddTransient<ChartDataAnalyzer>();
        services.AddTransient<MyStrategies>();

        return services;
    }
}