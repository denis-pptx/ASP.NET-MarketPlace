namespace MarketPlace.BLL.Interfaces;

public interface ICartService
{
    public Task<Response<Cart>> GetAsync(string customerLogin);
    public Task<Response<IEnumerable<CartItem>>> GetAsync(IEnumerable<int> cartItemsIds);
    public Task<Response<bool>> AddProductAsync(string customerLogin, int productId);
    public Task<Response<bool>> RemoveProductAsync(string customerLogin, int productId);
    public Task<Response<bool>> UpdateQuantity(string customerLogin, int productId, int quantity);
}
