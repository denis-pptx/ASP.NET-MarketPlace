using MarketPlace.BLL.Infrastracture;
using MarketPlace.BLL.Interfaces;
using MarketPlace.DAL.Entities;
using MarketPlace.DAL.Interfaces;

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

            return new()
            {
                StatusCode = StatusCode.OK,
                Data = customers
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

    public async Task<Response<bool>> CreateAsync(Customer item)
    {
        try
        {
            var customer = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(
                c => c.Login.Trim().ToLower() == item.Login.Trim().ToLower());

            if (customer != null)
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
                Data = true,
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

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        try
        {
            var customer = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null)
            {
                return new()
                {
                    Data = false,
                    Description = "Такого покупателя нет",
                    StatusCode = StatusCode.CustomerNotFound
                };
            }

            await _unitOfWork.CustomerRepository.DeleteAsync(customer);

            return new()
            {
                Data = true,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Data = false,
                StatusCode = StatusCode.InternalServerError,
                Description = ex.Message
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
                    Description = "Такого покупателя нет",
                    StatusCode = StatusCode.CustomerNotFound
                };
            }

            return new()
            {
                Data = customer,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = ex.Message
            };
        }
    }

    public async Task<Response<bool>> UpdateAsync(Customer item)
    {
        try
        {
            var customer = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(c => c.Login == item.Login);
            if (customer != null && customer.Id != item.Id)
            {
                return new()
                {
                    Description = "Логин занят",
                    StatusCode = StatusCode.LoginIsUsed
                };
            }

            customer = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(c => c.Id == item.Id);
            if (customer == null)
            {
                return new()
                {
                    Description = "Такого покупателя нет",
                    StatusCode = StatusCode.CustomerNotFound
                };
            }

            customer.Login = item.Login;
            customer.Password = item.Password;
            customer.Profile = item.Profile;

            await _unitOfWork.CustomerRepository.UpdateAsync(customer);

            return new()
            {
                StatusCode = StatusCode.OK,
                Data = true,
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
}
