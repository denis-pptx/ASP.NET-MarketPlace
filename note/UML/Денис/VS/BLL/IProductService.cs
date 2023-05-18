namespace MarketPlace.BLL.Interfaces;

public interface IProductService
{
    Task<Response<IEnumerable<Product>>> GetAsync();
    Task<Response<IEnumerable<Product>>> GetAsync(IEnumerable<ProductCategory> categories, string searchString);

    Task<Response<IEnumerable<Product>>> GetAsync(int shopId, IEnumerable<ProductCategory>? categories = null);
    Task<Response<IEnumerable<Product>>> GetBySellerLoginAsync(string sellerLogin);
    Task<Response<bool>> CreateAsync(Product product);

    Task<Response<bool>> DeleteAsync(int id);

    Task<Response<Product>> GetByIdAsync(int id);

    Task<Response<bool>> UpdateAsync(Product product);

    Response<SelectList> GetCategories();

    Response<SelectList> GetSelectListByCategories(IEnumerable<ProductCategory> categories);
}
