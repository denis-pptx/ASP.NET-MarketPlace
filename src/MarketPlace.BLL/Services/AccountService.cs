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
            var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync(u => u.Login == vm.Login);

            if (user == null)
            {
                return new()
                {
                    Description = "User not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            if (user.Password != vm.Password)
            {
                return new()
                {
                    Description = "Wrong password",
                    StatusCode = HttpStatusCode.Unauthorized
                };
            }

            return new()
            {
                Data = GetClaimsPrincipal(user),
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Description = ex.Message,
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<Response<ClaimsPrincipal>> RegisterAsync(Customer item)
    {
        try
        {
            var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync(
                u => u.Login.Trim().ToLowerInvariant() == item.Login.Trim().ToLowerInvariant());

            if (user != null)
            {
                return new()
                {
                    Description = "Login is already used",
                    StatusCode = HttpStatusCode.Conflict
                };
            }

            await _unitOfWork.CustomerProfileRepository.AddAsync(item.Profile);
            await _unitOfWork.CustomerRepository.AddAsync(item);
            
            return new()
            {
                Data = GetClaimsPrincipal(item),
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Description = ex.Message,
                StatusCode = HttpStatusCode.InternalServerError
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
