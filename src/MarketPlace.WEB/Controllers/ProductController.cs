using MarketPlace.DAL.Response;
using MarketPlace.WEB.Models;

namespace MarketPlace.WEB.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICartService _cartService;

    public ProductController(IProductService productService, ICartService cartService)
    {
        _productService = productService;
        _cartService = cartService;
    }

    public async Task<IActionResult> Index(IEnumerable<ProductCategory> categories, string searchString)
    {
        var productResponse = await _productService.GetAsync(categories, searchString);
        if (productResponse.StatusCode == HttpStatusCode.OK)
        {
            return View(new ProductListViewModel()
            {
                Products = productResponse.Data!.OrderBy(p => p.Category.GetDisplayName()).ThenBy(p => p.Name),
                AllCategories = _productService.GetCategories().Data!,
                SelectedCategories = _productService.GetSelectListByCategories(categories).Data!,
                SearchString = searchString
            });
        }
        return View("Error", new ErrorViewModel(productResponse.Deconstruct()));
    }

    public async Task<IActionResult> Details(int id)
    {
        var response = await _productService.GetByIdAsync(id);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return View(response.Data);
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
    }
}
