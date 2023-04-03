using MarketPlace.BLL.Infrastracture;
using MarketPlace.DAL.Entities;

namespace MarketPlace.BLL.Interfaces;

public interface IProductService
{
    Task<Response<IEnumerable<Product>>> GetAsync();

    Task<Response<IEnumerable<Product>>> GetByShopIdAsync(int shopId);

    Task<Response<bool>> CreateAsync(int shopId, Product product);

    Task<Response<bool>> DeleteAsync(int id);

    Task<Response<Product>> GetByIdAsync(int id);

    Task<Response<bool>> UpdateAsync(Product product);
}
