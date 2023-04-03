using MarketPlace.BLL.Infrastracture;
using MarketPlace.BLL.Interfaces;
using MarketPlace.BLL.ViewModels;
using MarketPlace.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

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

    public async Task<IActionResult> Index(int shopId = 0)
    {
        var sellerResponse = await _sellerService.GetByShopIdAsync(shopId);
        var shopResponse = await _shopService.GetAsync();

        if (sellerResponse.StatusCode == BLL.Infrastracture.StatusCode.OK &&
            shopResponse.StatusCode == BLL.Infrastracture.StatusCode.OK)
        {
            var shops = shopResponse.Data!.ToList();
            shops.Insert(0, new Shop() { Id = 0,  Name = "Все" });

            return View(new SellerListViewModel(sellerResponse!.Data!, shops, shopId));
        }
        return View("Error", $"{sellerResponse.Description}\n{shopResponse.Description}");
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var shopRespone = await _shopService.GetAsync();
        if (shopRespone.StatusCode == BLL.Infrastracture.StatusCode.OK)
        {
            return View(new SellerViewModel(shopRespone.Data!));
        }
        return View("Error", shopRespone.Description);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Seller item)
    {
        if (ModelState.IsValid)
        {
            var createResponse = await _sellerService.CreateAsync(item);
            if (createResponse.StatusCode == BLL.Infrastracture.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", createResponse.Description);
        }

        var shopRespone = await _shopService.GetAsync();
        if (shopRespone.StatusCode == BLL.Infrastracture.StatusCode.OK)
        {
            return View(new SellerViewModel(shopRespone.Data!, item));
        }
        return View("Error", shopRespone.Description);
    }


    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _sellerService.DeleteAsync(id);
        if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
        {
            return RedirectToAction("Index");
        }
        return View("Error", response.Description);
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var shopRespone = await _shopService.GetAsync();
        if (shopRespone.StatusCode == BLL.Infrastracture.StatusCode.OK)
        {
            var sellerResponse = await _sellerService.GetByIdAsync(id);
            if (sellerResponse.StatusCode == BLL.Infrastracture.StatusCode.OK)
            {
                sellerResponse.Data!.PasswordConfirm = sellerResponse.Data.Password;
                return View(new SellerViewModel(shopRespone.Data!, sellerResponse.Data!));
            }
            return View("Error", sellerResponse.Description);
        }
        return View("Error", shopRespone.Description);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Seller item)
    {
        if (ModelState.IsValid)
        {
            var response = await _sellerService.UpdateAsync(item);
            if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", response.Description);
        }

        var shopRespone = await _shopService.GetAsync();
        if (shopRespone.StatusCode == BLL.Infrastracture.StatusCode.OK)
        {
            return View(new SellerViewModel(shopRespone.Data!, item));
        }
        return View("Error", shopRespone.Description);
    }
}
