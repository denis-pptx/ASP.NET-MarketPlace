namespace MarketPlace.BLL.Interfaces;

public interface ISellerService
{
    Task<Response<IEnumerable<Seller>>> GetByShopIdAsync(int shopId);

    Task<Response<bool>> CreateAsync(Seller seller);

    Task<Response<bool>> DeleteAsync(int id);

    Task<Response<Seller>> GetByIdAsync(int id);

    Task<Response<bool>> UpdateAsync(Seller shop);

    Task<Response<int>> GetShopIdByLogin(string sellerLogin);
}
