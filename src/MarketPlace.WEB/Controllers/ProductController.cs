using MarketPlace.WEB.Models;

namespace MarketPlace.WEB.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index(IEnumerable<ProductCategory> categories, string searchString)
    {
        var response = await _productService.GetAsync(categories, searchString);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return View(new ProductListViewModel()
            {
                Products = response.Data!.OrderBy(p => p.Category.GetDisplayName()).ThenBy(p => p.Name),
                AllCategories = _productService.GetCategories().Data!,
                SelectedCategories = _productService.GetSelectListByCategories(categories).Data!,
                SearchString = searchString
            });
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
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
