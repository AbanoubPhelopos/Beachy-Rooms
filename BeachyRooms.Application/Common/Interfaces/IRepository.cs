using System.Linq.Expressions;

namespace BeachyRooms.Application.Common.Interfaces;

public interface IRepository<T>
{
    // Async methods for querying data
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    // Methods for data manipulation
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);

    // Check existence
    Task<bool> ExistsAsync(int id);
}