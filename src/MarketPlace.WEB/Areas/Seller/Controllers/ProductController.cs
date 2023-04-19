using MarketPlace.WEB.Areas.Seller.Models;

namespace MarketPlace.WEB.Areas.Seller.Controllers;

[Area("Seller")]
[Authorize(Roles = "Seller")]
public class ProductController : Controller
{
    private IProductService _productService;
    private ISellerService _sellerService;

    public ProductController(IProductService productService, ISellerService sellerService)
    {
        _productService = productService;
        _sellerService = sellerService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var sellerResponse = await _sellerService.GetShopIdByLogin(User.Identity?.Name ?? "");
        if (sellerResponse.StatusCode == HttpStatusCode.OK)
        {
            var productResponse = await _productService.GetByShopIdAsync(sellerResponse.Data);
            if (productResponse.StatusCode == HttpStatusCode.OK)
            {
                return View(new ProductListViewModel()
                {
                    Products = productResponse.Data!
                });
            }
            return View("Error", new ErrorViewModel(productResponse.Deconstruct()));
        }
        return View("Error", new ErrorViewModel(sellerResponse.Deconstruct()));
    }


    [HttpGet]
    public async Task<IActionResult> Save(int id)
    {
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
                var sellerResponse = await _sellerService.GetShopIdByLogin(User.Identity?.Name ?? "");
                if (sellerResponse.StatusCode == HttpStatusCode.OK)
                {
                    item.ShopId = sellerResponse.Data;
                    var productResponse = await _productService.CreateAsync(item);
                    if (productResponse.StatusCode == HttpStatusCode.OK)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", productResponse.Description);
                }
                ModelState.AddModelError("", sellerResponse.Description);
            }
            // Update.
            else
            {
                var response = await _productService.UpdateAsync(item);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", response.Description);
            }
        }

        return View(item);
    }


    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _productService.DeleteAsync(id);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return RedirectToAction("Index");
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
    }
}
