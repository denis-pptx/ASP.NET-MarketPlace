using MarketPlace.DAL.Data;
using MarketPlace.DAL.Entities;
using MarketPlace.DAL.Interfaces;

namespace MarketPlace.DAL.Repository;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<User> _userRepository;

    public EfUnitOfWork(AppDbContext db)
    {
        _db = db;
        _productRepository = new EfRepository<Product>(_db);
        _userRepository = new EfRepository<User>(_db);
    }

    public IRepository<Product> ProductRepository => _productRepository;

    public IRepository<User> UserRepository => _userRepository;

    public async Task CreateDatabaseAsync()
    {
        await _db.Database.EnsureCreatedAsync();
    }

    public async Task RemoveDatabaseAsync()
    {
       await _db.Database.EnsureDeletedAsync();
    }

    public async Task SaveAllAsync()
    {
        await _db.SaveChangesAsync();
    }
}
