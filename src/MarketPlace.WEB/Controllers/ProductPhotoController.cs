namespace MarketPlace.WEB.Controllers;

public class ProductPhotoController : Controller
{
    private readonly IProductPhotoService _productPhotoService;

    public ProductPhotoController(IProductPhotoService productPhotoService)
    {
        _productPhotoService = productPhotoService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int productId)
    {
        var response = await _productPhotoService.GetAsync(productId);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return PartialView("_Index", response.Data);
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
    }
}
