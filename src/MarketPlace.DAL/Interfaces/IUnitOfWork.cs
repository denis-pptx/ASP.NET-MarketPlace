namespace MarketPlace.DAL.Interfaces;

public interface IUnitOfWork
{
    IRepository<Product> ProductRepository { get; }
    IRepository<User> UserRepository { get; }
    IRepository<Shop> ShopRepository { get; }
    IRepository<Seller> SellerRepository { get; }
    IRepository<Customer> CustomerRepository { get; }
    IRepository<Cart> CartRepository { get; }
    IRepository<CartItem> CartItemRepository { get; }
    IRepository<ProductPhoto> ProductPhotoRepository { get; }
    Task RemoveDatabaseAsync();
    Task CreateDatabaseAsync();
    Task SaveAllAsync();
}
