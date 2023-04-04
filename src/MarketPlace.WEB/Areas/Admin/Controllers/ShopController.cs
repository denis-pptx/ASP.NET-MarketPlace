using MarketPlace.BLL.Interfaces;
using MarketPlace.BLL.Infrastracture;
using MarketPlace.BLL.ViewModels;
using MarketPlace.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MarketPlace.WEB.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = $"Admin")]
public class ShopController : Controller
{
    private IShopService _shopService;
    public ShopController(IShopService shopService)
    {
        _shopService = shopService;
    }


    public async Task<IActionResult> Index(string name = "")
    {
        var response = await _shopService.GetBySimilarNameAsync(name);
        if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
        {
            return View(new ShopListViewModel(response.Data!));
        }
        return View("Error", response.Description);
    }


    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(Shop item)
    {
        if (ModelState.IsValid)
        {
            var response = await _shopService.CreateAsync(item);
            if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", response.Description);
        }
        return View(item);
    }


    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _shopService.DeleteAsync(id);
        if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
        {
            return RedirectToAction("Index");
        }
        return View("Error", response.Description);
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var response = await _shopService.GetByIdAsync(id);
        if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
        {
            return View(response.Data);
        }
        return View("Error", response.Description);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Shop item)
    {
        if (ModelState.IsValid)
        {
            var response = await _shopService.UpdateAsync(item);
            if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", response.Description);
        }
        return View(item);
    }

    [HttpGet]
    public async Task<IActionResult> GetShop()
    {
        var sellerLogin = User.FindFirst(ClaimsIdentity.DefaultNameClaimType)?.Value ?? "";

        var response = await _shopService.GetBySellerLoginAsync(sellerLogin);
        if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
        {
            return RedirectToAction("Edit", response.Data);
        }
        return View("Error", response.Description);
    }
}
