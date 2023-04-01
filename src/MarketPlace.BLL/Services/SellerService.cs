using MarketPlace.BLL.Infrastracture;
using MarketPlace.BLL.Interfaces;
using MarketPlace.BLL.ViewModels;
using MarketPlace.DAL.Entities;
using MarketPlace.DAL.Interfaces;

namespace MarketPlace.BLL.Services;

public class SellerService : ISellerService
{
    private IUnitOfWork _unitOfWork;
    public SellerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<IEnumerable<Seller>>> GetByShopIdAsync(int? shopId)
    {
        try
        {
            Func<Seller, bool> filter = seller =>
            {
                if (shopId == null || shopId == 0)
                {
                    return true;
                }
                else
                {
                    return seller.ShopId == shopId;
                }
            };

            List<Seller> sellers = (await _unitOfWork.SellerRepository.ListAsync(filter)).ToList();

            for (int i = 0; i < sellers.Count; i++)
            {
                var shop = await _unitOfWork.ShopRepository.FirstOrDefaultAsync(shop => shop.Id == sellers[i].ShopId);
                if (shop == null)
                {
                    return new()
                    {
                        StatusCode = StatusCode.ShopNotFound,
                        Description = "Не найден такой магазин"
                    };
                }
                //sellers[i].Shop = new ShopViewModel()
                //{
                //    Id = shop.Id,
                //    Name = shop.Name,
                //    Description = shop.Description,
                //};
            }


            return new()
            {
                StatusCode = StatusCode.OK,
                Data = sellers
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = ex.Message
            };
        }
    }
}
