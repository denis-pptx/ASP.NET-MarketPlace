namespace MarketPlace.WEB.Areas.Seller.Controllers;

[Area("Seller")]
[Authorize(Roles = $"Seller")]
public class ShopController : Controller
{
    IShopService _shopService;
    public ShopController(IShopService shopService)
    {
        _shopService = shopService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var response = await _shopService.GetBySellerLoginAsync(User.Identity?.Name ?? "");
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return View(response.Data);
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
    }

    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        var response = await _shopService.GetBySellerLoginAsync(User.Identity?.Name ?? "");
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return View(response.Data);
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
    }


    [HttpPost]
    public async Task<IActionResult> Edit(Shop item)
    {
        if (ModelState.IsValid)
        {
            var response = await _shopService.UpdateAsync(item);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", response.Description);
        }
        return View(item);
    }
}
