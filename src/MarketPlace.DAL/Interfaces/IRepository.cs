namespace MarketPlace.DAL.Interfaces;

public interface IRepository<T> where T : Entity
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> ListAllAsync();
    Task<IEnumerable<T>> ListAsync(Func<T, bool> filter);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T?> FirstOrDefaultAsync(Func<T, bool> filter);
    Task<T?> SingleOrDefaultAsync(Func<T, bool> filter);
}
