using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.BLL.Interfaces;

public interface IOrderService
{
    public Task<Response<bool>> AddAsync(Order order);
    public Task<Response<bool>> AddAsync(IEnumerable<int> cartsItemsIds);
    public Task<Response<IEnumerable<Order>>> GetAsync(string customerLogin);
    public Task<Response<IEnumerable<Order>>> CreateAsync(IEnumerable<int> cartsItemsIds);
    public Task<Response<bool>> DeleteAsync(int id);
}
