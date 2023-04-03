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


    public async Task<Response<IEnumerable<Seller>>> GetByShopIdAsync(int shopId)
    {
        try
        {
            Func<Seller, bool> filter = seller =>
            {
                if (shopId == 0)
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

    public async Task<Response<bool>> CreateAsync(Seller item)
    {
        try
        {
            var seller = await _unitOfWork.SellerRepository.FirstOrDefaultAsync(
                s => s.Login.Trim().ToLower() == item.Login.Trim().ToLower());
            if (seller != null)
            {
                return new()
                {
                    Description = "Логин занят",
                    StatusCode = StatusCode.UserLoginIsUsed
                };
            }

            await _unitOfWork.SellerRepository.AddAsync(item);

            return new()
            {
                StatusCode = StatusCode.OK,
                Data = true,
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        try
        {
            var seller = await _unitOfWork.SellerRepository.FirstOrDefaultAsync(s => s.Id == id);
            if (seller == null)
            {
                return new()
                {
                    Data = false,
                    Description = "Такого продавца нет",
                    StatusCode = StatusCode.SellerNotFound
                };
            }

            await _unitOfWork.SellerRepository.DeleteAsync(seller);

            return new()
            {
                Data = true,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Data = false,
                StatusCode = StatusCode.InternalServerError,
                Description = ex.Message
            };
        }
    }

    public async Task<Response<Seller>> GetByIdAsync(int id)
    {
        try
        {
            var seller = await _unitOfWork.SellerRepository.FirstOrDefaultAsync(s => s.Id == id);
            if (seller == null)
            {
                return new()
                {
                    Description = "Такого продавца нет",
                    StatusCode = StatusCode.SellerNotFound
                };
            }

            return new()
            {
                Data = seller,
                StatusCode = StatusCode.OK
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

    public async Task<Response<bool>> UpdateAsync(Seller item)
    {
        try
        {
            var seller = await _unitOfWork.SellerRepository.FirstOrDefaultAsync(s => s.Id == item.Id);
            if (seller == null)
            {
                return new()
                {
                    Description = "Такого магазина нет",
                    StatusCode = StatusCode.SellerNotFound
                };
            }

            seller.Login = item.Login;
            seller.Password = item.Password;
            seller.ShopId = item.ShopId;
            seller.Shop = item.Shop;

            await _unitOfWork.SellerRepository.UpdateAsync(seller);

            return new()
            {
                StatusCode = StatusCode.OK,
                Data = true,
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}
