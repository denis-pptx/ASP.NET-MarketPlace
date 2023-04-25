namespace MarketPlace.DAL.Repository;

public class EfShopRepository : IRepository<Shop>
{
    private readonly AppDbContext _db;
    private readonly DbSet<Shop> _shops;

    public EfShopRepository(AppDbContext db)
    {
        _db = db;
        _shops = db.Set<Shop>();
    }

    public async Task AddAsync(Shop shop)
    {
        await _shops.AddAsync(shop);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Shop entity)
    {
        await Task.Run(() => _shops.Remove(entity));
        await _db.SaveChangesAsync();
    }

    public async Task<Shop?> FirstOrDefaultAsync(Func<Shop, bool> filter)
    {
        return await Task.Run(() => _shops.Include(s => s.Products)
                                             .FirstOrDefault(filter));
    }

    public async Task<Shop?> GetByIdAsync(int id)
    {
        return await Task.Run(() => _shops.Include(s => s.Products)
                                             .SingleOrDefault(s => s.Id == id));
    }

    public async Task<IEnumerable<Shop>> ListAllAsync()
    {
        return await _shops.Include(s => s.Products)
                              .ToListAsync();
    }

    public async Task<IEnumerable<Shop>> ListAsync(Func<Shop, bool> filter)
    {
        return await Task.Run(() => _shops.Include(s => s.Products)
                                             .Where(filter));
    }

    public async Task<Shop?> SingleOrDefaultAsync(Func<Shop, bool> filter)
    {
        return await Task.Run(() => _shops.Include(s => s.Products)
                                             .SingleOrDefault(filter));
    }

    public async Task UpdateAsync(Shop entity)
    {
        await Task.Run(() => _shops.Update(entity));
        await _db.SaveChangesAsync();
    }
}
