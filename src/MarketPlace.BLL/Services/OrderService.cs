using MarketPlace.DAL.Entities;
using System.Security.Cryptography;

namespace MarketPlace.BLL.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<bool>> AddAsync(Order order)
    {
        try
        {
            await _unitOfWork.OrderRepository.AddAsync(order);

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

    public async Task<Response<bool>> AddAsync(IEnumerable<int> cartsItemsIds)
    {
        try
        {
            var orderResponse = await this.CreateAsync(cartsItemsIds);
            if (orderResponse.StatusCode == HttpStatusCode.OK)
            {
                var orders = orderResponse.Data;

                foreach (var order in orders)
                    await _unitOfWork.OrderRepository.AddAsync(order);

                return new()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = true
                };

            }

            return new()
            {
                Description = orderResponse.Description,
                StatusCode = orderResponse.StatusCode
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

    public async Task<Response<IEnumerable<Order>>> CreateAsync(IEnumerable<int> cartsItemsIds)
    {
        try
        {
            var cartItems = await _unitOfWork.CartItemRepository.ListAsync(ci => cartsItemsIds.Contains(ci.Id));

            var orderItems = new List<OrderItem>();
            foreach (var cartItem in cartItems)
            {
                orderItems.Add(new()
                {
                    Name = cartItem.Product!.Name,
                    Description = cartItem.Product!.Description,
                    Price = cartItem.Product!.Price,
                    Quantity = cartItem.Product!.Quantity,
                    Category = cartItem.Product!.Category,
                    Photo = cartItem.Product!.Photo,
                    ProductId = cartItem.Product!.Id,
                });

                cartItem.Product.Quantity -= cartItem.Quantity;
            }

            var orders = new List<Order>();
            foreach (var cartItem in cartItems.DistinctBy(ci => ci.Product!.ShopId))
            {
                orders.Add(new()
                {
                    CustomerId = cartItem.Cart!.CustomerId,
                    ShopId = cartItem.Product!.ShopId,
                    Items = orderItems.Where(oi => cartItem.Product!.Shop!.Products.Any(p => p.Id == oi.ProductId)).ToList()
                });
            }

            
            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = orders
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
