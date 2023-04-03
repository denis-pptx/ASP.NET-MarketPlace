using MarketPlace.BLL.Infrastracture;
using MarketPlace.BLL.Interfaces;
using MarketPlace.BLL.ViewModels;
using MarketPlace.DAL.Entities;
using MarketPlace.DAL.Interfaces;
using System.Security.Claims;

namespace MarketPlace.BLL.Services;

public class AccountService : IAccountService
{
    private IUnitOfWork _unitOfWork;
    public AccountService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Response<ClaimsPrincipal>> LoginAsync(LoginViewModel vm)
    {
        try
        {
            var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(u => u.Login == vm.Login);

            if (user == null)
            {
                return new()
                {
                    Description = "Пользователь не найден",
                    StatusCode = StatusCode.UserNotFound
                };
            }

            if (user.Password != vm.Password)
            {
                return new()
                {
                    Description = "Неверный пароль",
                    StatusCode = StatusCode.WrongPassword
                };
            }

            return new()
            {
                StatusCode = StatusCode.OK,
                Data = GetClaimsPrincipal(user)
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<Response<ClaimsPrincipal>> RegisterAsync(Customer item)
    {
        try
        {
            var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(
                u => u.Login.Trim().ToLowerInvariant() == item.Login.Trim().ToLowerInvariant());

            if (user != null)
            {
                return new()
                {
                    Description = "Логин занят",
                    StatusCode = StatusCode.LoginIsUsed
                };
            }

            await _unitOfWork.CustomerRepository.AddAsync(item);
            
            return new()
            {
                StatusCode = StatusCode.OK,
                Data = GetClaimsPrincipal(item)
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    private ClaimsPrincipal GetClaimsPrincipal(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
        };
        return new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies"));
    }
}
