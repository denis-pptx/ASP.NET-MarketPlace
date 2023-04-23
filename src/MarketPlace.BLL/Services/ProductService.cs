using MarketPlace.BLL.Extensions;
using MarketPlace.DAL.Entities;
using MarketPlace.DAL.Enum;
using Microsoft.AspNetCore.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                StatusCode = HttpStatusCode.OK,
                Data = products
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

    public async Task<Response<IEnumerable<Product>>> GetAsync(
        IEnumerable<ProductCategory> categories, string? searchString)
    {
        try
        {
            searchString = searchString ?? string.Empty;

            var products = await _unitOfWork.ProductRepository.ListAsync(
                p => categories.Count() == 0 || categories.Contains(p.Category));

            products = products.Where(p => searchString == string.Empty 
                                    || p.Name.IsSimilar(searchString));

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = products
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

    public async Task<Response<IEnumerable<Product>>> GetByShopIdAsync(int shopId)
    {
        try
        {
            var shop = await _unitOfWork.ShopRepository.SingleOrDefaultAsync(s => s.Id == shopId);
            if (shop == null)
            {
                return new()
                {
                    Description = "Shop not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var products = await _unitOfWork.ProductRepository.ListAsync(p => p.ShopId == shopId);

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = products
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

    public async Task<Response<IEnumerable<Product>>> GetByShopAndCategoryAsync(int shopId, int categoryId)
    {
        try
        {
            var productResponse = await this.GetByShopIdAsync(shopId);
            if (productResponse.StatusCode == HttpStatusCode.OK)
            {
                return new()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = productResponse.Data!.Where(p => categoryId == 0 || (int)p.Category == categoryId)
                };
            }

            return productResponse;
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

    public async Task<Response<bool>> CreateAsync(Product product)
    {
        try
        {
            var shop = await _unitOfWork.ShopRepository.SingleOrDefaultAsync(s => s.Id == product.ShopId);
            if (shop == null)
            {
                return new()
                {
                    Description = "Shop not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            await _unitOfWork.ProductRepository.AddAsync(product);

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
            var product = await _unitOfWork.ProductRepository.SingleOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return new()
                {
                    Description = "Product not found",
                    StatusCode = HttpStatusCode.NotFound,
                    Data = false
                };
            }

            await _unitOfWork.ProductRepository.DeleteAsync(product);

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = true
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Description = ex.Message,
                StatusCode = HttpStatusCode.InternalServerError,
                Data = false
            };
        }
    }

    public async Task<Response<bool>> UpdateAsync(Product item)
    {
        try
        {
            var product = await _unitOfWork.ProductRepository.SingleOrDefaultAsync(p => p.Id == item.Id);
            if (product == null)
            {
                return new()
                {
                    Description = "Product not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            await _unitOfWork.ProductRepository.UpdateAsync(item);

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

    public async Task<Response<Product>> GetByIdAsync(int id)
    {
        try
        {
            var product = await _unitOfWork.ProductRepository.SingleOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return new()
                {
                    Description = "Product not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = product,
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

    public Response<SelectList> GetCategories()
    {
        try
        {
            var response = this.GetSelectListByCategories((IEnumerable<ProductCategory>)Enum.GetValues(typeof(ProductCategory)));
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return new()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = response.Data
                };
            }

            return new()
            {
                Description = response.Description,
                StatusCode = response.StatusCode,
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

    public Response<SelectList> GetSelectListByCategories(IEnumerable<ProductCategory> list)
    {
        try
        {
            var categories = from type in list
                             orderby type.GetDisplayName()
                             select new
                             {
                                 Id = (int)type,
                                 Name = type.GetDisplayName()
                             };

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = new SelectList(categories, "Id", "Name")
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
