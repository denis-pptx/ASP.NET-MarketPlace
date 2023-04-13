using MarketPlace.BLL.DTO;

namespace MarketPlace.BLL.Interfaces;

public interface IAccountService
{
    Task<Response<ClaimsPrincipal>> RegisterAsync(Customer customer);

    Task<Response<ClaimsPrincipal>> LoginAsync(LoginDTO dto);
}
