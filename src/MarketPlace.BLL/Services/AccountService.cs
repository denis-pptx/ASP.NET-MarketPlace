namespace MarketPlace.BLL.Services;

public class AccountService : IAccountService
{
    private IUnitOfWork _unitOfWork;
    public AccountService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Response<ClaimsPrincipal>> LoginAsync(LoginDTO dto)
    {
        try
        {
            var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync(u => u.Login == dto.Login);

            if (user == null)
            {
                return new()
                {
                    Description = "User not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            if (user.Password != dto.Password)
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

    public async Task<Response<ClaimsPrincipal>> RegisterAsync(RegisterDTO dto)
    {
        try
        {
            var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync(
                u => u.Login.Trim().ToLowerInvariant() == dto.Login.Trim().ToLowerInvariant());

            if (user != null)
            {
                return new()
                {
                    Description = "Login is already used",
                    StatusCode = HttpStatusCode.Conflict
                };
            }

            Customer customer = new()
            {
                Login = dto.Login,
                Password = dto.Password,
                Role = Role.Customer,
                Profile = new()
                {
                    Age = dto.Age,
                    Email = dto.Email,
                    Phone = dto.Phone
                }
            };

            await _unitOfWork.CustomerRepository.AddAsync(customer);
            
            return new()
            {
                Data = GetClaimsPrincipal(customer),
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
