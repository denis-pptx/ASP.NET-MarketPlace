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
        return View("Error", new ErrorViewModel(response.StatusCode, response.Description));
    }

    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        var response = await _shopService.GetBySellerLoginAsync(User.Identity?.Name ?? "");
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return View(response.Data);
        }
        return View("Error", new ErrorViewModel(response.StatusCode, response.Description));
    }


    [HttpPost]
    public async Task<IActionResult> Edit(Shop item)
    {
        if (ModelState.IsValid)
        {
            var getResponse = await _shopService.GetBySellerLoginAsync(User.Identity?.Name ?? "");
            if (getResponse.StatusCode == HttpStatusCode.OK)
            {
                item.Id = getResponse.Data!.Id;
                var updateResponse = await _shopService.UpdateAsync(item);

                if (updateResponse.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", updateResponse.Description);
            }
            else
            {
                return View("Error", new ErrorViewModel(getResponse.StatusCode, getResponse.Description));
            }  
        }
        return View(item);
    }
}
