namespace MarketPlace.BLL.Services;

public class ProductPhotoService : IProductPhotoService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductPhotoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<IEnumerable<ProductPhoto>>> GetAsync(int productId)
    {
        try
        {
            var product = await _unitOfWork.ProductRepository.SingleOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                return new()
                {
                    Description = "Product not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            return new()
            {
                StatusCode = HttpStatusCode.OK,
                Data = product.Photos
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
