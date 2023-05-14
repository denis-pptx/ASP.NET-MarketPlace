namespace MarketPlace.BLL.Interfaces;

public interface IProductPhotoService
{
    Task<Response<IEnumerable<ProductPhoto>>> GetAsync(int productId);
}
