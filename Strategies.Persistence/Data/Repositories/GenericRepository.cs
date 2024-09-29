// Create a internal abstract class GenericRepository
using System.Linq.Expressions;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Strategies.Domain.Persistence;

namespace Strategies.Persistence;


internal class GenericRepository<T> : IRepository<T> where T : class
{
    protected StrategiesContext _context;
    protected readonly DbSet<T> _dbset;

    public GenericRepository(StrategiesContext context)
    {
        _context = context;
        _dbset = _context.Set<T>();
    }

    public virtual IEnumerable<T> GetAll()
    {
        return _dbset.AsEnumerable<T>();
    }

    public virtual IQueryable<T> GetAllQueryable()
    {
        return _dbset.AsQueryable<T>();
    }

    public virtual IQueryable<T> GetAllQueryableAsNoTracking()
    {
        return _dbset.AsQueryable<T>().AsNoTracking();
    }


    public virtual T? GetById(int id)
    {
        return _dbset.Find(id);
    }

    public virtual void Insert(T entity)
    {
        _dbset.Add(entity);
    }

    public virtual void Update(T entity)
    {
        _dbset.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Update(T existingEntity, T newEntityValues)
    {
        // Ensure the entity is being tracked by the context
        var entityEntry = _context.Entry(existingEntity);
        if (entityEntry.State == EntityState.Detached)
        {
            _dbset.Attach(existingEntity);
        }

        // Update the existing entity with new values
        entityEntry.CurrentValues.SetValues(newEntityValues);
    }

    public virtual void Delete(T entity)
    {
        _dbset.Remove(entity);
    }

    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
    {
        return _dbset.Where(predicate);
    }

    public virtual void SaveChanges()
    {
        Console.WriteLine("Saving changes...");
        _context.SaveChanges();
    }

    public virtual void AddRange(IEnumerable<T> entities)
    {
        _dbset.AddRange(entities);
    }

    // Method to add a range of data using BulkInsert
    public virtual void BulkAddRange(IEnumerable<T> entities)
    {
        _context.BulkInsert(entities);
    }

    public virtual void BulkInsertOrUpdate(IEnumerable<T> entities)
    {
        _context.BulkInsertOrUpdate(entities);
    }
}