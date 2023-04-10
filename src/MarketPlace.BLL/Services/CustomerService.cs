namespace MarketPlace.BLL.Services;

public class CustomerService : ICustomerService
{
    private IUnitOfWork _unitOfWork;
    public CustomerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<IEnumerable<Customer>>> GetAsync()
    {
        try
        {
            var customers = await _unitOfWork.CustomerRepository.ListAllAsync();
            foreach (var customer in customers)
            {
                var profile = await _unitOfWork.CustomerProfileRepository
                    .FirstOrDefaultAsync(cp => cp.CustomerId == customer.Id);

                if (profile == null) 
                {
                    return new()
                    {
                        Description = "Customer's profile not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }
                customer.Profile = profile;
            }

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = customers
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

    public async Task<Response<bool>> CreateAsync(Customer item)
    {
        try
        {
            var customer = await _unitOfWork.UserRepository.FirstOrDefaultAsync(
                c => c.Login.Trim().ToLower() == item.Login.Trim().ToLower());

            if (customer != null)
            {
                return new()
                {
                    Description = "Login is already used",
                    StatusCode = HttpStatusCode.Conflict
                };
            }

            await _unitOfWork.CustomerRepository.AddAsync(item);

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = true,
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

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        try
        {
            var customer = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null)
            {
                return new()
                {
                    Description = "Customer not found",
                    StatusCode = HttpStatusCode.NotFound,
                    Data = false
                };
            }

            await _unitOfWork.CustomerRepository.DeleteAsync(customer);

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = true
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Description = ex.Message,
                StatusCode = HttpStatusCode.InternalServerError,
                Data = false
            };
        }
    }

    public async Task<Response<Customer>> GetByIdAsync(int id)
    {
        try
        {
            var customer = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null)
            {
                return new()
                {
                    Description = "Customer not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var profile = await _unitOfWork.CustomerProfileRepository
                .FirstOrDefaultAsync(cp => cp.CustomerId == customer.Id);
            if (profile == null)
            {
                return new()
                {
                    Description = "Customer's profile not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }
            customer.Profile = profile;

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = customer
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

    public async Task<Response<bool>> UpdateAsync(Customer item)
    {
        try
        {
            var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(u => u.Login == item.Login);
            if (user != null && user.Id != item.Id)
            {
                return new()
                {
                    Description = "Login is already used",
                    StatusCode = HttpStatusCode.Conflict
                };
            }

            var customer = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(c => c.Id == item.Id);
            if (customer == null)
            {
                return new()
                {
                    Description = "Customer not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            await _unitOfWork.CustomerRepository.UpdateAsync(item);

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = true,
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
}
