namespace Strategies.Domain.Persistence;

/// <summary>
/// The Unit of Work pattern used to group together all the operations that are performed on the database.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IRepository<T> Repository<T>() where T : class; // Method to get a repository for a specific entity type.
    void SaveChanges();
    Task<int> SaveChangesAsync();
}