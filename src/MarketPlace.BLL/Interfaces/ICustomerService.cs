namespace MarketPlace.BLL.Interfaces;

public interface ICustomerService
{
    Task<Response<IEnumerable<Customer>>> GetAsync();

    Task<Response<bool>> CreateAsync(Customer seller);

    Task<Response<bool>> DeleteAsync(int id);

    Task<Response<Customer>> GetByIdAsync(int id);

    Task<Response<bool>> UpdateAsync(Customer shop);
}
