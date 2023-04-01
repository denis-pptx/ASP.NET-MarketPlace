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

   
    public async Task<Response<IEnumerable<ShopViewModel>>> GetAsync()
    {
        try
        {
            var shops = from shop in await _unitOfWork.ShopRepository.ListAllAsync()
                        select new ShopViewModel()
                        {
                            Id = shop.Id,
                            Name = shop.Name,
                            Description = shop.Description
                        };

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

    public async Task<Response<bool>> CreateAsync(ShopViewModel vm)
    {
        try
        {
            var shop = await _unitOfWork.ShopRepository.FirstOrDefaultAsync(s => s.Name == vm.Name);
            if (shop != null)
            {
                return new()
                {
                    Description = "Имя магазина занято",
                    StatusCode = StatusCode.ShopNameIsUsed
                };
            }

            shop = new()
            {
                Name = vm.Name,
                Description = vm.Description
            };

            await _unitOfWork.ShopRepository.AddAsync(shop);

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

    public async Task<Response<ShopViewModel>> GetByIdAsync(int id)
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
                Data = new ShopViewModel()
                {
                    Id = shop.Id,
                    Name = shop.Name,
                    Description = shop.Description,
                },
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

    public async Task<Response<bool>> UpdateAsync(ShopViewModel vm)
    {
        try
        {
            var shop = await _unitOfWork.ShopRepository.FirstOrDefaultAsync(s => s.Id == vm.Id);
            if (shop == null)
            {
                return new()
                {
                    Description = "Такого магазина нет",
                    StatusCode = StatusCode.ShopNotFound
                };
            }

            shop = new Shop()
            {
                Id = vm.Id,
                Name = vm.Name,
                Description = vm.Description
            };

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
