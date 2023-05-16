using MarketPlace.DAL.Entities;
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

    public async Task<Response<IEnumerable<CartItem>>> GetAsync(IEnumerable<int> cartItemsIds)
    {
        try
        {
            var cartItems = await _unitOfWork.CartItemRepository.ListAsync(ci => cartItemsIds.Contains(ci.Id));

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = cartItems
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
    public async Task<Response<bool>> AddProductAsync(string customerLogin, int productId)
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

            var product = await _unitOfWork.ProductRepository.SingleOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                return new()
                {
                    Description = "Product not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            if (customer.Cart!.Items.Any(i => i.ProductId == product.Id))
            {
                return new()
                {
                    Description = "Cart already contains this product",
                    StatusCode = HttpStatusCode.Conflict
                };
            }

            customer.Cart.Items.Add(new CartItem()
            {
                Quantity = 1,
                ProductId = product.Id
            });

            await _unitOfWork.SaveAllAsync();

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

    public async Task<Response<bool>> RemoveProductAsync(string customerLogin, int productId)
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

            
            if (!customer.Cart!.Items.Any(i => i.ProductId == productId))
            {
                return new()
                {
                    Description = "Customer cart doesn't contains cart item",
                    StatusCode = HttpStatusCode.Conflict
                };
            }

            var cartItem = await _unitOfWork.CartItemRepository.SingleOrDefaultAsync(i => i.ProductId == productId);
            if (cartItem == null)
            {
                return new()
                {
                    Description = "Cart item not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            await _unitOfWork.CartItemRepository.DeleteAsync(cartItem);

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

    public async Task<Response<bool>> UpdateQuantity(string customerLogin, int productId, int quantity)
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

            var product = await _unitOfWork.ProductRepository.SingleOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                return new()
                {
                    Description = "Product not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            
            if (!customer.Cart!.Items.Any(i => i.ProductId == product.Id))
            {
                return new()
                {
                    Description = "Cart doesn't contain this product",
                    StatusCode = HttpStatusCode.Conflict
                };
            }

            if (quantity > product.Quantity)
            {
                return new()
                {
                    Description = "New quantity is more than product stock",
                    StatusCode = HttpStatusCode.Conflict
                };
            }

            var existingCartItem = customer.Cart.Items.SingleOrDefault(ci => ci.ProductId == productId);
            if (existingCartItem == null)
            {
                return new()
                {
                    Description = "Cart item not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            existingCartItem.Quantity = quantity;
            await _unitOfWork.SaveAllAsync();

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

    
}
