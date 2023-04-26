namespace MarketPlace.DAL.Repository;

public class EfCustomerRepository : IRepository<Customer>
{
    private readonly AppDbContext _db;
    private readonly DbSet<Customer> _customers;

    public EfCustomerRepository(AppDbContext db)
    {
        _db = db;
        _customers = db.Set<Customer>();
    }

    public async Task AddAsync(Customer customer)
    {
        await _customers.AddAsync(customer);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Customer customer)
    {
        await Task.Run(() => _customers.Remove(customer));
        await _db.SaveChangesAsync();
    }

    public async Task<Customer?> FirstOrDefaultAsync(Func<Customer, bool> filter)
    {
        return await Task.Run(() => _customers.Include(c => c.Profile)
                                              .FirstOrDefault(filter));
    }

    public async Task<Customer?> SingleOrDefaultAsync(Func<Customer, bool> filter)
    {
        return await Task.Run(() => _customers.Include(c => c.Profile)
                                              .SingleOrDefault(filter));
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await Task.Run(() => _customers.Include(c => c.Profile)
                                              .SingleOrDefault(c => c.Id == id));
    }

    public async Task<IEnumerable<Customer>> ListAllAsync()
    {
        return await _customers.Include(c => c.Profile)
                               .ToListAsync();
    }

    public async Task<IEnumerable<Customer>> ListAsync(Func<Customer, bool> filter)
    {
        return await Task.Run(() => _customers.Include(c => c.Profile)
                                              .Where(filter));
    }

    public async Task UpdateAsync(Customer customer)
    {
        await Task.Run(() => _customers.Update(customer));
        await _db.SaveChangesAsync();
    }
}
