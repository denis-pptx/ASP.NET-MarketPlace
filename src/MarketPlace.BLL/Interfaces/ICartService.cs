namespace MarketPlace.BLL.Interfaces;

public interface ICartService
{
    public Task<Response<Cart>> GetAsync(string customerLogin);
    public Task<Response<bool>> AddProduct(string customerLogin, int productId);
    public Task<Response<bool>> RemoveProduct(string customerLogin, int productId);
}
