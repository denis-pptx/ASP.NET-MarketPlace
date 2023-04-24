using System.Runtime.CompilerServices;

namespace MarketPlace.BLL.Services;

public class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;
    public CartService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<Response<Cart>> GetAsync(string customerLogin)
    {
        try
        {
            var customer = await _unitOfWork.CustomerRepository.SingleOrDefaultAsync(c => c.Login == customerLogin);
            if (customer == null)
            {
                return new()
                {
                    Description = "Customer not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var cart = await _unitOfWork.CartRepository.SingleOrDefaultAsync(c => c.CustomerId == customer.Id);
            if (cart == null)
            {
                return new()
                {
                    Description = "Cart not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }


            //cart.Products = (from cp in await _unitOfWork.CartProductRepository.ListAllAsync()
            //                 join p in await _unitOfWork.ProductRepository.ListAllAsync()
            //                 on cp.ProductId equals p.Id
            //                 where cp.CartId == cart.Id
            //                 select p).ToList();


            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = cart
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

    public async Task<Response<bool>> AddProduct(string customerLogin, int productId)
    {
        try
        {
            var customer = await _unitOfWork.CustomerRepository.SingleOrDefaultAsync(c => c.Login == customerLogin);
            if (customer == null)
            {
                return new()
                {
                    Description = "Customer not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var cart = await _unitOfWork.CartRepository.SingleOrDefaultAsync(c => c.CustomerId == customer.Id);
            if (cart == null)
            {
                return new()
                {
                    Description = "Cart not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var product = await _unitOfWork.ProductRepository.SingleOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                return new()
                {
                    Description = "Product not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            //cart.Products = (from cp in await _unitOfWork.CartProductRepository.ListAllAsync()
            //                 join p in await _unitOfWork.ProductRepository.ListAllAsync()
            //                 on cp.ProductId equals p.Id
            //                 where cp.CartId == cart.Id
            //                 select p).ToList();

            if (cart.Products.Contains(product))
            {
                return new()
                {
                    Description = "Cart already contains this product",
                    StatusCode = HttpStatusCode.Conflict
                };
            }

            cart.Products.Add(product);
            await _unitOfWork.CartRepository.UpdateAsync(cart);

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
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public Task<Response<bool>> RemoveProduct(string customerLogin, int productId)
    {
        throw new NotImplementedException();
    }
}
