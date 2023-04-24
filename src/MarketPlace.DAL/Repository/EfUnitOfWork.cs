namespace MarketPlace.DAL.Repository;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Shop> _shopRepository;
    private readonly IRepository<Seller> _sellerRepository;
    private readonly IRepository<Customer> _customerRepository;
    private readonly IRepository<CustomerProfile> _customerProfileRepository;
    private readonly IRepository<Cart> _cartRepository;
    private readonly IRepository<CartProduct> _cartProductRepository;

    public EfUnitOfWork(AppDbContext db)
    {
        _db = db;
        _productRepository = new EfRepository<Product>(_db);
        _userRepository = new EfRepository<User>(_db);
        _shopRepository = new EfRepository<Shop>(_db);
        _sellerRepository = new EfRepository<Seller>(_db);
        _customerRepository = new EfRepository<Customer>(_db);
        _customerProfileRepository = new EfRepository<CustomerProfile>(_db);
        _cartRepository = new EfRepository<Cart>(_db);
        _cartProductRepository = new EfRepository<CartProduct>(_db);
    }

    public IRepository<Product> ProductRepository => _productRepository;

    public IRepository<User> UserRepository => _userRepository;

    public IRepository<Shop> ShopRepository => _shopRepository;
    public IRepository<Seller> SellerRepository => _sellerRepository;

    public IRepository<Customer> CustomerRepository => _customerRepository;
    public IRepository<CustomerProfile> CustomerProfileRepository => _customerProfileRepository;
    public IRepository<Cart> CartRepository => _cartRepository;
    public IRepository<CartProduct> CartProductRepository => _cartProductRepository;

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
