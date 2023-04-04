using MarketPlace.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        var sellerLogin = User.FindFirst(ClaimsIdentity.DefaultNameClaimType)?.Value ?? "";

        var response = await _shopService.GetBySellerLoginAsync(sellerLogin);
        if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
        {
            return View(response.Data);
        }
        return View("Error", response.Description);
    }
}
