namespace MarketPlace.BLL.Interfaces;

public interface IAccountService
{
    Task<Response<ClaimsPrincipal>> RegisterAsync(RegisterDTO dto);

    Task<Response<ClaimsPrincipal>> LoginAsync(LoginDTO dto);
}
