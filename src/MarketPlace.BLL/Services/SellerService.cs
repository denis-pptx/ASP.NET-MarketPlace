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

            IEnumerable<Seller> sellers = await _unitOfWork.SellerRepository.ListAsync(filter);

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
