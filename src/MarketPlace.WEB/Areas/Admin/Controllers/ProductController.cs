using MarketPlace.WEB.Areas.Admin.Models;

namespace MarketPlace.WEB.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly IShopService _shopService;

    public ProductController(IProductService productService, IShopService shopService)
    {
        _productService = productService;
        _shopService = shopService;
    }


    public async Task<IActionResult> Index(int shopId, ProductCategory category, SortOrder order)
    {
        var response = await _productService.GetByShopIdAsync(shopId);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return View(new ProductListViewModel()
            {
                Products = response.Data!.Where(p => category == 0 || p.Category == category).Sort(order),
                Categories = response.Data!.GetCategories(),
                Category = category,
                Order = order,
                ShopId = shopId
            });
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));

    }

    [HttpGet]
    public async Task<IActionResult> Save(int id, int shopId)
    {
        // Create.
        if (id == 0)
        {
            return View(new Product()
            {
                ShopId = shopId
            });
        }

        // Update.
        var response = await _productService.GetByIdAsync(id);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return View(response.Data!);
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save([Bind("Id,Name,Description,Price,Category,ShopId")] Product item)
    {
        if (ModelState.IsValid)
        {
            // Create.
            if (item.Id == 0)
            {
                var productResponse = await _productService.CreateAsync(item);
                if (productResponse.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("Index", new { shopId = item.ShopId });
                }
                ModelState.AddModelError("", productResponse.Description);
            }
            // Update.
            else
            {
                var response = await _productService.UpdateAsync(item);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("Index", new { shopId = item.ShopId });
                }
                ModelState.AddModelError("", response.Description);
            }
        }

        return View("Save", item);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, int shopId)
    {
        var response = await _productService.DeleteAsync(id);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return RedirectToAction("Index", new { shopId });
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
    }
}
