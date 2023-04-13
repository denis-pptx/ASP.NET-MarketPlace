using MarketPlace.BLL.Extensions;

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
            foreach(var shop in shops)
            {
                shop.Products = (await _unitOfWork.ProductRepository
                    .ListAsync(p => p.ShopId == shop.Id)).ToList();
            }

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = shops
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Description = ex.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<Response<IEnumerable<Shop>>> GetBySimilarNameAsync(string? name)
    {
        try
        {
            name = name == null ? string.Empty : name;

            Func<Shop, bool> filter = shop => shop.Name.RemoveWhitespaces().ToLowerInvariant().
                                                Contains(name.RemoveWhitespaces().ToLowerInvariant());

            var shops = await _unitOfWork.ShopRepository.ListAsync(filter);
            foreach (var shop in shops)
            {
                shop.Products = (await _unitOfWork.ProductRepository
                    .ListAsync(p => p.ShopId == shop.Id)).ToList();
            }

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = shops
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Description = ex.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<Response<Shop>> GetBySellerLoginAsync(string sellerLogin)
    {
        try
        {
            var seller = await _unitOfWork.SellerRepository.SingleOrDefaultAsync(s => s.Login == sellerLogin);
            if (seller == null)
            {
                return new()
                {
                    Description = "Seller not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var shop = await _unitOfWork.ShopRepository.SingleOrDefaultAsync(s => s.Id == seller.ShopId);
            if (shop == null)
            {
                return new()
                {
                    Description = "Seller's shop not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }
            seller.Shop = shop;

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = seller.Shop
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Description = ex.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<Response<bool>> CreateAsync(Shop item)
    {
        try
        {
            var shop = await _unitOfWork.ShopRepository.SingleOrDefaultAsync(
                s => s.Name.Trim().ToLower() == item.Name.Trim().ToLower());
            if (shop != null)
            {
                return new()
                {
                    Description = "Shop name is already used",
                    StatusCode = HttpStatusCode.Conflict
                };
            }

            await _unitOfWork.ShopRepository.AddAsync(item);

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = true,
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Description = ex.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        try
        {
            var shop = await _unitOfWork.ShopRepository.SingleOrDefaultAsync(s => s.Id == id);
            if (shop == null)
            {
                return new()
                {
                    Description = "Shop not found",
                    StatusCode = HttpStatusCode.NotFound,
                    Data = false
                };
            }

            await _unitOfWork.ShopRepository.DeleteAsync(shop);

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = true
            };
        }
        catch(Exception ex)
        {
            return new()
            {
                Description = ex.Message,
                StatusCode = HttpStatusCode.InternalServerError,
                Data = false
            };
        }
    }

    public async Task<Response<Shop>> GetByIdAsync(int id)
    {
        try
        {
            var shop = await _unitOfWork.ShopRepository.SingleOrDefaultAsync(s => s.Id == id);
            if (shop == null)
            {
                return new()
                {
                    Description = "Shop not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = shop
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Description = ex.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<Response<bool>> UpdateAsync(Shop item)
    {
        try
        {
            var shop = await _unitOfWork.ShopRepository.SingleOrDefaultAsync(s => s.Name == item.Name);
            if (shop != null && shop.Id != item.Id)
            {
                return new()
                {
                    Description = "Shop name is already used",
                    StatusCode = HttpStatusCode.Conflict
                };
            }

            shop = await _unitOfWork.ShopRepository.SingleOrDefaultAsync(s => s.Id == item.Id);
            if (shop == null)
            {
                return new()
                {
                    Description = "Shop not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            await _unitOfWork.ShopRepository.UpdateAsync(item);

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = true,
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Description = ex.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    
}
