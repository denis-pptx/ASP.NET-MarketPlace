using MarketPlace.BLL.Infrastracture;
using MarketPlace.BLL.ViewModels;
using System.Security.Claims;

namespace MarketPlace.BLL.Interfaces;

public interface IAccountService
{
    Task<Response<ClaimsPrincipal>> RegisterAsync(RegisterViewModel vm);

    Task<Response<ClaimsPrincipal>> LoginAsync(LoginViewModel vm);
}
