using Microsoft.EntityFrameworkCore;
using Strategies.Domain;

namespace Strategies.Persistence;

/// <summary>
/// This class is responsible for configuring the database context and the entity to table mapping.
/// </summary>
public class StrategiesContext : DbContext
{
    // This context has a constructor allowing it to receives and configure a connection string from CreateHostBuilder() in the console app.
    public StrategiesContext(DbContextOptions<StrategiesContext> options) : base(options)
    {
    }

    public DbSet<Candle> Candles { get; set; }
    public DbSet<Results> Results { get; set; }
    public DbSet<Trade> Trades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Candle Entity
        modelBuilder.Entity<Candle>().HasKey(c => new { c.Symbol, c.Exchange, c.Resolution, c.DateDownloaded, c.Date });
        // Since the primary key already serves as an index, additional indexing might not be necessary unless there are specific query patterns that would benefit from it

        // Configure Result Entity.
        modelBuilder.Entity<Results>().HasKey(r => r.Id);
        // Since the primary key already serves as an index, additional indexing might not be necessary unless there are specific query patterns that would benefit from it

        // Configure Trade Entity.
        modelBuilder.Entity<Trade>().HasKey(t => new { t.ResultsId, t.Start});

        // Configure the postgresql database to use the timestamp without time zone
        modelBuilder.Entity<Candle>(entity =>
                {
                    entity.Property(e => e.Date).HasColumnType("timestamp without time zone");
                    entity.Property(e => e.DateDownloaded).HasColumnType("timestamp without time zone");
                });
    }
}
