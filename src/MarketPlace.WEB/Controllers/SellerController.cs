using MarketPlace.BLL.Interfaces;
using MarketPlace.BLL.ViewModels;
using MarketPlace.DAL.Entities;
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
            ViewBag.Shops = new SelectList(shopRespone.Data, "Id", "Name");
            return View();
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
            ViewBag.Shops = new SelectList(shopRespone.Data!.OrderBy(s => s.Name), "Id", "Name");
            return View(item);
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
            ViewBag.Shops = new SelectList(shopRespone.Data!.OrderBy(s => s.Name), "Id", "Name");

            var sellerResponse = await _sellerService.GetByIdAsync(id);
            if (sellerResponse.StatusCode == BLL.Infrastracture.StatusCode.OK)
            {
                return View(sellerResponse.Data);
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
            ViewBag.Shops = new SelectList(shopRespone.Data!.OrderBy(s => s.Name), "Id", "Name");
            return View(item);
        }
        return View("Error", shopRespone.Description);
    }
}
