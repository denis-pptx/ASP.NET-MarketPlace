using MarketPlace.WEB.Areas.Seller.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MarketPlace.WEB.Areas.Seller.Controllers;

[Area("Seller")]
[Authorize(Roles = "Seller")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ISellerService _sellerService;
    private readonly IShopService _shopService;
    public ProductController(IProductService productService,
        ISellerService sellerService, IShopService shopService)
    {
        _productService = productService;
        _sellerService = sellerService;
        _shopService = shopService;
    }

    public async Task<IActionResult> Index(ProductCategory category, SortOrder order)
    {
        var response = await _productService.GetBySellerLoginAsync(User.Identity!.Name!);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return View(new ProductListViewModel()
            {
                Products = response.Data!.Where(p => category == 0 || p.Category == category).Sort(order),
                Categories = response.Data!.GetCategories(),
                Category = category,
                Order = order
            });
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
    }

    [HttpGet]
    public async Task<IActionResult> Save(int id)
    {
        // Create.
        if (id == 0)
        {
            return View(new Product());
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
    public async Task<IActionResult> Save([Bind("Id,Name,Description,Price,Category")] Product item, IFormFile? photo)
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
                    item.Photo = photo?.ToByteArray() ?? null;

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
                item.Photo = photo?.ToByteArray() ?? item.Photo;
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
