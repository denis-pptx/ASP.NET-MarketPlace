using MarketPlace.BLL.Infrastracture;
using MarketPlace.BLL.Interfaces;
using MarketPlace.BLL.ViewModels;
using MarketPlace.DAL.Entities;
using MarketPlace.DAL.Interfaces;

namespace MarketPlace.BLL.Services;

public class ShopService : IShopService
{
    private IUnitOfWork _unitOfWork;
    public ShopService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

   
    public async Task<Response<IEnumerable<Shop>>> GetAsync()
    {
        try
        {
            var shops = await _unitOfWork.ShopRepository.ListAllAsync();

            return new()
            {
                StatusCode = StatusCode.OK,
                Data = shops
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

    public async Task<Response<bool>> CreateAsync(Shop item)
    {
        try
        {
            var shop = await _unitOfWork.ShopRepository.FirstOrDefaultAsync(s => s.Name == item.Name);
            if (shop != null)
            {
                return new()
                {
                    Description = "Имя магазина занято",
                    StatusCode = StatusCode.ShopNameIsUsed
                };
            }

            await _unitOfWork.ShopRepository.AddAsync(item);

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
            var shop = await _unitOfWork.ShopRepository.FirstOrDefaultAsync(s => s.Id == id);
            if (shop == null)
            {
                return new()
                {
                    Data = false,
                    Description = "Такого магазина нет",
                    StatusCode = StatusCode.ShopNotFound
                };
            }

            await _unitOfWork.ShopRepository.DeleteAsync(shop);

            return new()
            {
                Data = true,
                StatusCode = StatusCode.OK
            };
        }
        catch(Exception ex)
        {
            return new()
            {
                Data = false,
                StatusCode = StatusCode.InternalServerError,
                Description = ex.Message
            };
        }
    }

    public async Task<Response<Shop>> GetByIdAsync(int id)
    {
        try
        {
            var shop = await _unitOfWork.ShopRepository.FirstOrDefaultAsync(s => s.Id == id);
            if (shop == null)
            {
                return new()
                {
                    Description = "Такого магазина нет",
                    StatusCode = StatusCode.ShopNotFound
                };
            }

            return new()
            {
                Data = shop,
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

    public async Task<Response<bool>> UpdateAsync(Shop item)
    {
        try
        {
            var shop = await _unitOfWork.ShopRepository.FirstOrDefaultAsync(s => s.Id == item.Id);
            if (shop == null)
            {
                return new()
                {
                    Description = "Такого магазина нет",
                    StatusCode = StatusCode.ShopNotFound
                };
            }

            shop.Id = item.Id;
            shop.Name = item.Name;
            shop.Description = item.Description;
            shop.Products = item.Products;

            await _unitOfWork.ShopRepository.UpdateAsync(shop);

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
