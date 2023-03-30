using MarketPlace.DAL.Entities;

namespace MarketPlace.DAL.Interfaces;

public interface IRepository<T> where T : Entity
{
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<IReadOnlyList<T>> ListAsync(Func<T, bool> filter);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T?> FirstOrDefaultAsync(Func<T, bool> filter);
}
