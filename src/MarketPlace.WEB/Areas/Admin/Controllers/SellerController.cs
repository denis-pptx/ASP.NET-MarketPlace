namespace MarketPlace.WEB.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = $"Admin")]
public class SellerController : Controller
{
    private ISellerService _sellerService;
    private IShopService _shopService;
    public SellerController(ISellerService sellerService, IShopService shopService)
    {
        _sellerService = sellerService;
        _shopService = shopService;
    }

    public async Task<IActionResult> Index(int shopId = 0)
    {
        var sellerResponse = await _sellerService.GetByShopIdAsync(shopId);
        var shopResponse = await _shopService.GetAsync();

        if (sellerResponse.StatusCode == HttpStatusCode.OK &&
            shopResponse.StatusCode == HttpStatusCode.OK)
        {
            var shops = shopResponse.Data!.ToList();
            shops.Insert(0, new Shop() { Id = 0, Name = "Все" });

            return View(new SellerListViewModel(sellerResponse!.Data!, shops, shopId));
        }
        return View("Error", $"{sellerResponse.Description}\n{shopResponse.Description}");
    }


    [HttpGet]
    public async Task<IActionResult> Save(int id)
    {
        var shopRespone = await _shopService.GetAsync();
        if (shopRespone.StatusCode == HttpStatusCode.OK)
        {
            if (id == 0)
            {
                return View(new SellerViewModel(shopRespone.Data!));
            }

            var sellerResponse = await _sellerService.GetByIdAsync(id);
            if (sellerResponse.StatusCode == HttpStatusCode.OK)
            {
                sellerResponse.Data!.PasswordConfirm = sellerResponse.Data.Password;
                return View(new SellerViewModel(shopRespone.Data!, sellerResponse.Data!));
            }
            return View("Error", sellerResponse.Description);
        }
        return View("Error", shopRespone.Description);
    }


    [HttpPost]
    public async Task<IActionResult> Save(DAL.Entities.Seller item)
    {
        if (ModelState.IsValid)
        {
            // Create.
            if (item.Id == 0)
            {
                var createResponse = await _sellerService.CreateAsync(item);
                if (createResponse.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", createResponse.Description);
            }
            // Update.
            else
            {
                var updateResponse = await _sellerService.UpdateAsync(item);
                if (updateResponse.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", updateResponse.Description);
            }
        }

        var shopRespone = await _shopService.GetAsync();
        if (shopRespone.StatusCode == HttpStatusCode.OK)
        {
            return View(new SellerViewModel(shopRespone.Data!, item));
        }
        return View("Error", shopRespone.Description);
    }


    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _sellerService.DeleteAsync(id);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return RedirectToAction("Index");
        }
        return View("Error", response.Description);
    }
}
