using MarketPlace.BLL.Interfaces;
using MarketPlace.BLL.Infrastracture;
using MarketPlace.BLL.ViewModels;
using MarketPlace.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MarketPlace.BLL.Services;

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
    public async Task<IActionResult> Save(int id)
    {
        // Create.
        if (id == 0)
        {
            return View();
        }

        // Update.
        var response = await _shopService.GetByIdAsync(id);
        if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
        {
            return View(response.Data);
        }
        return View("Error", response.Description);
    }


    [HttpPost]
    public async Task<IActionResult> Save(Shop item)
    {
        if (ModelState.IsValid)
        {
            // Create.
            if (item.Id == 0)
            {
                var response = await _shopService.CreateAsync(item);
                if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", response.Description);
            }
            // Update.
            else
            {
                var response = await _shopService.UpdateAsync(item);
                if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
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
        var response = await _shopService.DeleteAsync(id);
        if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
        {
            return RedirectToAction("Index");
        }
        return View("Error", response.Description);
    }
}
