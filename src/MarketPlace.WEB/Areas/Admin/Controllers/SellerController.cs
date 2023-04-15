using MarketPlace.DAL.Response;

namespace MarketPlace.WEB.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SellerController : Controller
{
    private ISellerService _sellerService;
    private IShopService _shopService;
    public SellerController(ISellerService sellerService, IShopService shopService)
    {
        _sellerService = sellerService;
        _shopService = shopService;
    }

    public async Task<IActionResult> Index(int shopId)
    {
        var sellerResponse = await _sellerService.GetByShopIdAsync(shopId);
        if (sellerResponse.StatusCode == HttpStatusCode.OK)
        {
            var shopResponse = await _shopService.GetAsync();
            if (shopResponse.StatusCode == HttpStatusCode.OK)
            {
                return View(new SellerListViewModel()
                {
                    Sellers = sellerResponse.Data!.OrderBy(s => s.Login),
                    Shops = new SelectList(shopResponse.Data!.OrderBy(s => s.Name), "Id", "Name"),
                    ShopId = shopId
                });
            }
            return View("Error", new ErrorViewModel(shopResponse.Deconstruct()));
        }
        return View("Error", new ErrorViewModel(sellerResponse.Deconstruct()));
    }


    [HttpGet]
    public async Task<IActionResult> Save(int id)
    {
        var shopRespone = await _shopService.GetAsync();
        if (shopRespone.StatusCode == HttpStatusCode.OK)
        {
            var shopSelectList = new SelectList(shopRespone.Data!.OrderBy(s => s.Name), "Id", "Name");

            if (id == 0)
            {
                return View(new SellerViewModel()
                {
                    Shops = shopSelectList
                });
            }

            var sellerResponse = await _sellerService.GetByIdAsync(id);
            if (sellerResponse.StatusCode == HttpStatusCode.OK)
            {
                return View(new SellerViewModel()
                {
                    Seller = sellerResponse.Data!,
                    Shops = shopSelectList
                });
            }
            return View("Error", new ErrorViewModel(sellerResponse.Deconstruct()));
        }
        return View("Error", new ErrorViewModel(shopRespone.Deconstruct()));
    }


    [HttpPost]
    public async Task<IActionResult> Save(DAL.Entities.Seller item)
    {
        ModelState.Remove("PasswordConfirm");

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
            return View(new SellerViewModel()
            {
                Shops = new SelectList(shopRespone.Data!.OrderBy(s => s.Name), "Id", "Name"),
                Seller = item
            });
        }
        return View("Error", new ErrorViewModel(shopRespone.Deconstruct()));
    }


    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _sellerService.DeleteAsync(id);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return RedirectToAction("Index");
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
    }
}
