namespace MarketPlace.DAL.Repository;

public class EfCartRepository : IRepository<Cart>
{
    private readonly AppDbContext _db;
    private readonly DbSet<Cart> _carts;

    public EfCartRepository(AppDbContext db)
    {
        _db = db;
        _carts = db.Set<Cart>();
    }

    public async Task AddAsync(Cart cart)
    {
        await _carts.AddAsync(cart);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Cart cart)
    {
        await Task.Run(() => _carts.Remove(cart));
        await _db.SaveChangesAsync();
    }

    public async Task<Cart?> FirstOrDefaultAsync(Func<Cart, bool> filter)
    {
        return await Task.Run(() => _carts.Include(c => c.Products)
                                          .FirstOrDefault(filter));
    }

    public async Task<Cart?> GetByIdAsync(int id)
    {
        return await Task.Run(() => _carts.Include(c => c.Products)
                                          .SingleOrDefault(c => c.Id == id));
    }

    public async Task<IEnumerable<Cart>> ListAllAsync()
    {
        return await _carts.Include(c => c.Products)
                           .ToListAsync();
    }

    public async Task<IEnumerable<Cart>> ListAsync(Func<Cart, bool> filter)
    {
        return await Task.Run(() => _carts.Include(c => c.Products)
                                          .Where(filter));
    }

    public async Task<Cart?> SingleOrDefaultAsync(Func<Cart, bool> filter)
    {
        return await Task.Run(() => _carts.Include(c => c.Products)
                                          .SingleOrDefault(filter));
    }

    public async Task UpdateAsync(Cart cart)
    {
        await Task.Run(() => _carts.Update(cart));
        await _db.SaveChangesAsync();
    }
}
