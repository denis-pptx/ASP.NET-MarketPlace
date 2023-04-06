namespace MarketPlace.BLL.Interfaces;

public interface IAccountService
{
    Task<Response<ClaimsPrincipal>> RegisterAsync(Customer customer);

    Task<Response<ClaimsPrincipal>> LoginAsync(LoginViewModel vm);
}
