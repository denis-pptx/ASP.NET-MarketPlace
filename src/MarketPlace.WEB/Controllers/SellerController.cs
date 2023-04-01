using MarketPlace.BLL.Interfaces;
using MarketPlace.BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MarketPlace.WEB.Controllers;

public class SellerController : Controller
{
    private ISellerService _sellerService;
    private IShopService _shopService;
    public SellerController(ISellerService sellerService, IShopService shopService)
    {
        _sellerService = sellerService;
        _shopService = shopService;
    }

    public async Task<IActionResult> Index(int? shopId)
    {

        var sellerResponse = await _sellerService.GetByShopIdAsync(shopId);
        var shopResponse = await _shopService.GetAsync();

        if (sellerResponse.StatusCode == BLL.Infrastracture.StatusCode.OK &&
            shopResponse.StatusCode == BLL.Infrastracture.StatusCode.OK)
        {
            var shops = shopResponse.Data!.ToList();
            shops.Insert(0, new ShopViewModel()
            {
                Id = 0,
                Name = "Все"
            });

            return View(new SellerListViewModel() 
            {
                Sellers = sellerResponse!.Data!,
                Shops = new SelectList(shops, "Id", "Name", shopId),
            });
        }
        return View("Error", $"{sellerResponse.Description}\n{shopResponse.Description}");
    }
}
