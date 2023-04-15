namespace MarketPlace.WEB.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ProductController : Controller
{
    private IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int shopId)
    {
        var response = await _productService.GetByShopIdAsync(shopId);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            ViewData["shopId"] = shopId;

            return View(new ProductListViewModel()
            {
                Products = response.Data!
            });
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
    }


    [HttpGet]
    public async Task<IActionResult> Save(int id, int shopId)
    {
        ViewData["shopId"] = shopId;

        // Create.
        if (id == 0)
        {
            return View();
        }

        // Update.
        var response = await _productService.GetByIdAsync(id);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return View(response.Data);
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
    }


    [HttpPost]
    public async Task<IActionResult> Save(Product item)
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

        ViewData["shopId"] = item.ShopId;
        return View(item);
    }


    [HttpPost]
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
