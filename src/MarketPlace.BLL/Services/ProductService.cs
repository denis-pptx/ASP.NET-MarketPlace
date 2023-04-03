using MarketPlace.BLL.Infrastracture;
using MarketPlace.BLL.Interfaces;
using MarketPlace.DAL.Entities;
using MarketPlace.DAL.Interfaces;

namespace MarketPlace.BLL.Services;

public class ProductService : IProductService
{
    private IUnitOfWork _unitOfWork;
    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<Response<IEnumerable<Product>>> GetAsync()
    {
        try
        {
            var products = await _unitOfWork.ProductRepository.ListAllAsync();

            return new()
            {
                StatusCode = StatusCode.OK,
                Data = products
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

    public async Task<Response<IEnumerable<Product>>> GetByShopIdAsync(int shopId)
    {
        try
        {
            var products = await _unitOfWork.ProductRepository.ListAsync(p => p.ShopId == shopId);

            return new()
            {
                StatusCode = StatusCode.OK,
                Data = products
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

    public async Task<Response<bool>> CreateAsync(int shopId, Product product)
    {
        try
        {
            var shop = await _unitOfWork.ShopRepository.FirstOrDefaultAsync(s => s.Id == shopId);

            if (shop == null)
            {
                return new()
                {
                    StatusCode = StatusCode.ShopNotFound,
                    Description = "Магазин не найден"
                };
            }

            product.ShopId = shopId;
            await _unitOfWork.ProductRepository.AddAsync(product);
            
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
            var product = await _unitOfWork.ProductRepository.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return new()
                {
                    Data = false,
                    Description = "Такого продукта нет",
                    StatusCode = StatusCode.ProductNotFound
                };
            }

            await _unitOfWork.ProductRepository.DeleteAsync(product);

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

    public async Task<Response<bool>> UpdateAsync(Product item)
    {
        try
        {
            var product = await _unitOfWork.ProductRepository.FirstOrDefaultAsync(p => p.Id == item.Id);
            if (product == null)
            {
                return new()
                {
                    Description = "Такого товара нет",
                    StatusCode = StatusCode.ProductNotFound
                };
            }

            product.Name = item.Name;
            product.Description = item.Description;
            product.Price = item.Price;

            await _unitOfWork.ProductRepository.UpdateAsync(product);

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

    public async Task<Response<Product>> GetByIdAsync(int id)
    {
        try
        {
            var product = await _unitOfWork.ProductRepository.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return new()
                {
                    Description = "Товар не найден",
                    StatusCode = StatusCode.ProductNotFound
                };
            }

            return new()
            {
                Data = product,
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
}
