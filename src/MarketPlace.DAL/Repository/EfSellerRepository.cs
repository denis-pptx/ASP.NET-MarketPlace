namespace MarketPlace.DAL.Repository;

public class EfSellerRepository : IRepository<Seller>
{
    private readonly AppDbContext _db;
    private readonly DbSet<Seller> _sellers;

    public EfSellerRepository(AppDbContext db)
    {
        _db = db;
        _sellers = db.Set<Seller>();
    }

    public async Task AddAsync(Seller seller)
    {
        await _sellers.AddAsync(seller);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Seller seller)
    {
        await Task.Run(() => _sellers.Remove(seller));
        await _db.SaveChangesAsync();
    }

    public async Task<Seller?> FirstOrDefaultAsync(Func<Seller, bool> filter)
    {
        return await Task.Run(() => _sellers.Include(s => s.Shop)
                                                .ThenInclude(s => s.Products)
                                            .FirstOrDefault(filter));
    }

    public async Task<Seller?> GetByIdAsync(int id)
    {
        return await Task.Run(() => _sellers.Include(s => s.Shop)
                                                .ThenInclude(s => s.Products)
                                            .SingleOrDefault(s => s.Id == id));
    }

    public async Task<IEnumerable<Seller>> ListAllAsync()
    {
        return await _sellers.Include(s => s.Shop)
                                .ThenInclude(s => s.Products)
                             .ToListAsync();
    }

    public async Task<IEnumerable<Seller>> ListAsync(Func<Seller, bool> filter)
    {
        return await Task.Run(() => _sellers.Include(s => s.Shop)
                                                .ThenInclude(s => s.Products)
                                            .Where(filter));
    }

    public async Task<Seller?> SingleOrDefaultAsync(Func<Seller, bool> filter)
    {
        return await Task.Run(() => _sellers.Include(s => s.Shop)
                                                .ThenInclude(s => s.Products)
                                            .SingleOrDefault(filter));
    }

    public async Task UpdateAsync(Seller seller)
    {
        await Task.Run(() => _sellers.Update(seller));
        await _db.SaveChangesAsync();
    }
}
