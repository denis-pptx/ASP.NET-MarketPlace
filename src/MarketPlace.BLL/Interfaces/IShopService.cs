using MarketPlace.BLL.Infrastracture;
using MarketPlace.BLL.ViewModels;
using MarketPlace.DAL.Entities;

namespace MarketPlace.BLL.Interfaces;

public interface IShopService
{
    Task<Response<IEnumerable<ShopViewModel>>> GetAsync();

    Task<Response<bool>> CreateAsync(ShopViewModel vm);

    Task<Response<bool>> DeleteAsync(int id);

    Task<Response<ShopViewModel>> GetByIdAsync(int id);

    Task<Response<bool>> UpdateAsync(ShopViewModel vm);
}
