using MarketPlace.BLL.Interfaces;
using MarketPlace.BLL.Services;
using MarketPlace.BLL.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.WEB.Controllers
{
    public class ShopController : Controller
    {
        private IShopService _shopService;
        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _shopService.GetAsync();
            if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", response.Description);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ShopViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var response = await _shopService.CreateAsync(vm);
                if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(vm);
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
        public async Task<IActionResult> Edit(ShopViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var response = await _shopService.UpdateAsync(vm);
                if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(vm);
        }
    }
}
