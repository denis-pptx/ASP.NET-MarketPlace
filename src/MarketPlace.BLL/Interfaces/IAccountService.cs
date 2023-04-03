using MarketPlace.BLL.Infrastracture;
using MarketPlace.BLL.ViewModels;
using MarketPlace.DAL.Entities;
using System.Security.Claims;

namespace MarketPlace.BLL.Interfaces;

public interface IAccountService
{
    Task<Response<ClaimsPrincipal>> RegisterAsync(Customer customer);

    Task<Response<ClaimsPrincipal>> LoginAsync(LoginViewModel vm);
}
