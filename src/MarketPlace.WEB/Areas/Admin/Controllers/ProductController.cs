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
        var productResponse = await _productService.GetAsync(shopId, 
            category == 0 ? null : new List<ProductCategory> { category });

        var categoriesResponse = await _shopService.GetCategoriesByIdAsync(shopId);


        if (new List<BaseResponse>() { productResponse, categoriesResponse }
            .Any(r => r.StatusCode != HttpStatusCode.OK))
        {
            return View("Error", new ErrorViewModel(productResponse.Deconstruct()));
        }
       
        return View(new ProductListViewModel()
        {
            Products = productResponse.Data.Sort(order),
            Categories = categoriesResponse.Data,
            Category = category,
            Order = order,
            ShopId = shopId
        });
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
