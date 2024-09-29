using Strategies.Domain.Persistence;

namespace Strategies.Persistence;


/// <summary>
/// This class is responsible for managing the repositories and the database context.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    // A DbContext instance which represents a session with the database. Allows for querying and saving data.
    private readonly StrategiesContext _context;

    // Constructor. This DbContext is typically your EF database context.
    public UnitOfWork(StrategiesContext context)
    {
        _context = context;
    }

    // Creates and returns a new GenericRepository for a given entity type.
    // This allows for CRUD operations specific to that entity type.
    public IRepository<T> Repository<T>() where T : class
    {
        // return _serviceProvider.GetRequiredService<IRepository<T>>();
        return new GenericRepository<T>(_context);
    }

    // Saves all changes made in this unit of work (i.e., the current DbContext) to the database.
    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    // Asynchronous version of SaveChanges
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    // Disposes the DbContext to free up resources.
    // IDisposable is implemented to ensure this can be used with 'using' statements for automatic cleanup.
    public void Dispose()
    {
        _context.Dispose();
    }
}