using MarketPlace.BLL.Infrastracture;
using MarketPlace.BLL.Interfaces;
using MarketPlace.BLL.Services;
using MarketPlace.BLL.ViewModels;
using MarketPlace.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MarketPlace.WEB.Areas.Seller.Controllers;

[Area("Seller")]
[Authorize(Roles = $"Seller")]
public class ProductController : Controller
{
    private IProductService _productService;
    private ISellerService _sellerService;

    private string _getSellerLogin()
    {
        return User.FindFirst(ClaimsIdentity.DefaultNameClaimType)?.Value ?? "";
    }

    public ProductController(IProductService productService, ISellerService sellerService)
    {
        _productService = productService;
        _sellerService = sellerService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var sellerResponse = await _sellerService.GetShopIdByLogin(_getSellerLogin());
        if (sellerResponse.StatusCode == BLL.Infrastracture.StatusCode.OK)
        {
            var productResponse = await _productService.GetByShopIdAsync(sellerResponse.Data);
            if (productResponse.StatusCode == BLL.Infrastracture.StatusCode.OK)
            {
                return View(new ProductListViewModel(productResponse.Data!));
            }
            return View("Error", productResponse.Description);
        }
        return View("Error", sellerResponse.Description);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(Product item)
    {
        if (ModelState.IsValid)
        {
            var sellerResponse = await _sellerService.GetShopIdByLogin(_getSellerLogin());
            if (sellerResponse.StatusCode == BLL.Infrastracture.StatusCode.OK)
            {
                var productResponse = await _productService.CreateAsync(sellerResponse.Data, item);
                if (productResponse.StatusCode == BLL.Infrastracture.StatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", productResponse.Description);
            }
            ModelState.AddModelError("", sellerResponse.Description);

        }
        return View(item);
    }


    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _productService.DeleteAsync(id);
        if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
        {
            return RedirectToAction("Index");
        }
        return View("Error", response.Description);
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var response = await _productService.GetByIdAsync(id);
        if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
        {
            return View(response.Data);
        }
        return View("Error", response.Description);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Product item)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.UpdateAsync(item);
            if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", response.Description);
        }
        return View(item);
    }
}
