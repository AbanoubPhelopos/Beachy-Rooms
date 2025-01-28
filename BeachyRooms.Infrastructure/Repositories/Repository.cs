using System.Linq.Expressions;
using BeachyRooms.Application.Common.Interfaces;
using BeachyRooms.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BeachyRooms.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _entities;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    // Async methods for querying data
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _entities.AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _entities.FindAsync(id);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _entities.AsNoTracking().Where(predicate).ToListAsync();
    }

    // Methods for data manipulation
    public void Add(T entity)
    {
        _entities.Add(entity);
    }

    public void Update(T entity)
    {
        _entities.Update(entity);
    }

    public void Remove(T entity)
    {
        _entities.Remove(entity);
    }

    // Check existence
    public async Task<bool> ExistsAsync(int id)
    {
        var entity = await _entities.FindAsync(id);
        return entity != null;
    }
}