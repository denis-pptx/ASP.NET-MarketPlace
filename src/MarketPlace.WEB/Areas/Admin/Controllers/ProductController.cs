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


    public async Task<IActionResult> Index(int shopId, int categoryId)
    {
        //var productResponse = await _productService.GetByShopAndCategoryAsync(shopId, categoryId);
        //if (productResponse.StatusCode == HttpStatusCode.OK)
        //{
        //    var shopResponse = await _shopService.GetCategoriesByIdAsync(shopId);
        //    if (shopResponse.StatusCode == HttpStatusCode.OK)
        //    {
        //        return View(new ProductListViewModel()
        //        {
        //            Products = productResponse.Data!.
        //                OrderBy(p => p.Category.GetDisplayName()).ThenBy(p => p.Name),

        //            Categories = shopResponse.Data!,
        //            CategoryId = categoryId,

        //            ShopId = shopId
        //        });
        //    }
        //    return View("Error", new ErrorViewModel(shopResponse.Deconstruct()));
        //}
        //return View("Error", new ErrorViewModel(productResponse.Deconstruct()));
        return View();

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
