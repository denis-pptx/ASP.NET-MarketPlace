using System.Linq;

namespace MarketPlace.DAL.Repository;

public class EfCustomerRepository : IRepository<Customer>
{
    protected readonly AppDbContext _db;
    protected readonly DbSet<Customer> _customers;

    public EfCustomerRepository(AppDbContext db)
    {
        _db = db;
        _customers = db.Set<Customer>();
    }

    public async Task AddAsync(Customer entity)
    {
        await _customers.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Customer entity)
    {
        await Task.Run(() => _customers.Remove(entity));
        await _db.SaveChangesAsync();
    }

    public async Task<Customer?> FirstOrDefaultAsync(Func<Customer, bool> filter)
    {
        return await Task.Run(() => _customers.Include(c => c.Profile)
                                              .Include(c => c.Cart)
                                                  .ThenInclude(c => c!.Products)
                                              .FirstOrDefault(filter));
    }

    public async Task<Customer?> SingleOrDefaultAsync(Func<Customer, bool> filter)
    {
        return await Task.Run(() => _customers.Include(c => c.Profile)
                                              .Include(c => c.Cart)
                                                  .ThenInclude(c => c!.Products)
                                              .SingleOrDefault(filter));
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await Task.Run(() => _customers.Include(c => c.Profile)
                                              .Include(c => c.Cart)
                                                  .ThenInclude(c => c!.Products)
                                              .SingleOrDefault(c => c.Id == id));
    }

    public async Task<IEnumerable<Customer>> ListAllAsync()
    {
        return await _customers.Include(c => c.Profile)
                               .Include(c => c.Cart)
                                   .ThenInclude(c => c!.Products)
                               .ToListAsync();
    }

    public async Task<IEnumerable<Customer>> ListAsync(Func<Customer, bool> filter)
    {
        return await Task.Run(() => _customers.Include(c => c.Profile)
                                              .Include(c => c.Cart)
                                                  .ThenInclude(c => c!.Products)
                                              .Where(filter));
    }

    public async Task UpdateAsync(Customer entity)
    {
        await Task.Run(() => _customers.Update(entity));
        await _db.SaveChangesAsync();
    }
}
