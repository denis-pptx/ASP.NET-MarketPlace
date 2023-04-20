namespace MarketPlace.BLL.Interfaces;

public interface IProductService
{
    Task<Response<IEnumerable<Product>>> GetAsync();

    Task<Response<IEnumerable<Product>>> GetByShopIdAsync(int shopId);
    Task<Response<IEnumerable<Product>>> GetByShopAndCategoryAsync(int shopId, int categoryId);

    Task<Response<bool>> CreateAsync(Product product);

    Task<Response<bool>> DeleteAsync(int id);

    Task<Response<Product>> GetByIdAsync(int id);

    Task<Response<bool>> UpdateAsync(Product product);

    Response<SelectList> GetCategories();
}
