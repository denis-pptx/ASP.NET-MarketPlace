namespace MarketPlace.BLL.Services;

public class SellerService : ISellerService
{
    private IUnitOfWork _unitOfWork;
    public SellerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<Response<IEnumerable<Seller>>> GetByShopIdAsync(int shopId)
    {
        try
        {
            IEnumerable<Seller> sellers = await _unitOfWork.SellerRepository.
                ListAsync(s => shopId == 0 || s.ShopId == shopId);

            foreach (var seller in sellers)
            {
                var shop = await _unitOfWork.ShopRepository.SingleOrDefaultAsync(s => s.Id == seller.ShopId);
                if (shop == null)
                {
                    return new()
                    {
                        Description = "Seller's shop not found",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }
                seller.Shop = shop;
            }

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = sellers
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

    public async Task<Response<bool>> CreateAsync(Seller item)
    {
        try
        {
            var seller = await _unitOfWork.UserRepository.SingleOrDefaultAsync(
                s => s.Login.Trim().ToLower() == item.Login.Trim().ToLower());
            if (seller != null)
            {
                return new()
                {
                    Description = "Login is already used",
                    StatusCode = HttpStatusCode.Conflict
                };
            }

            item.Role = Role.Seller;
            await _unitOfWork.SellerRepository.AddAsync(item);

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
            var seller = await _unitOfWork.SellerRepository.SingleOrDefaultAsync(s => s.Id == id);
            if (seller == null)
            {
                return new()
                {
                    Description = "Seller not found",
                    StatusCode = HttpStatusCode.NotFound,
                    Data = false
                };
            }

            await _unitOfWork.SellerRepository.DeleteAsync(seller);

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

    public async Task<Response<Seller>> GetByIdAsync(int id)
    {
        try
        {
            var seller = await _unitOfWork.SellerRepository.SingleOrDefaultAsync(s => s.Id == id);
            if (seller == null)
            {
                return new()
                {
                    Description = "Seller not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var shop = await _unitOfWork.ShopRepository.SingleOrDefaultAsync(s => s.Id == seller.ShopId);
            if (shop == null)
            {
                return new()
                {
                    Description = "Seller's shop not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }
            seller.Shop = shop;

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = seller
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

    public async Task<Response<bool>> UpdateAsync(Seller item)
    {
        try
        {
            var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync(u => u.Login == item.Login);
            if (user != null && user.Id != item.Id)
            {
                return new()
                {
                    Description = "Login is already used",
                    StatusCode = HttpStatusCode.Conflict
                };
            }

            var existingSeller = await _unitOfWork.SellerRepository.SingleOrDefaultAsync(s => s.Id == item.Id);
            if (existingSeller == null)
            {
                return new()
                {
                    Description = "Seller not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            existingSeller.Login = item.Login;
            existingSeller.Password = item.Password;
            existingSeller.ShopId = item.ShopId;

            await _unitOfWork.SellerRepository.UpdateAsync(existingSeller);

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

    public async Task<Response<int>> GetShopIdByLogin(string sellerLogin)
    {
        try
        {
            var seller = await _unitOfWork.SellerRepository.SingleOrDefaultAsync(s => s.Login == sellerLogin);
            if (seller == null)
            {
                return new()
                {
                    Description = "Seller not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = seller.ShopId
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
