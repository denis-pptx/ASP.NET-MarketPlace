using MarketPlace.DAL.Entities;

namespace MarketPlace.DAL.Interfaces;

public interface IUnitOfWork
{
    IRepository<Product> ProductRepository { get; }
    IRepository<User> UserRepository { get; }
    IRepository<Shop> ShopRepository { get; }
    IRepository<Seller> SellerRepository { get; }
    Task RemoveDatabaseAsync();
    Task CreateDatabaseAsync();
    Task SaveAllAsync();
}
