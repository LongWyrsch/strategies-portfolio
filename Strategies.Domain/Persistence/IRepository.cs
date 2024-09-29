using System.Linq.Expressions;

namespace Strategies.Domain.Persistence;

/// <summary>
/// Interface for a generic repository.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    IQueryable<T> GetAllQueryable();
    IQueryable<T> GetAllQueryableAsNoTracking();
    T? GetById(int id);
    void Insert(T entity);
    void Update(T entity);
    void Update(T existingEntity, T newEntityValues);
    void Delete(T entity);
    void AddRange(IEnumerable<T> entities);
    void BulkAddRange(IEnumerable<T> entities);
    void BulkInsertOrUpdate(IEnumerable<T> entities);
    IEnumerable<T> Find(Expression<Func<T, bool>> predicate); // The `predicate` is the condition to check, determining if the value is returned or not.
    void SaveChanges();
}
