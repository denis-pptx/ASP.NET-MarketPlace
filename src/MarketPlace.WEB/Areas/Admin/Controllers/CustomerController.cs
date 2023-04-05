using MarketPlace.BLL.Interfaces;
using MarketPlace.BLL.Services;
using MarketPlace.BLL.ViewModels;
using MarketPlace.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace MarketPlace.WEB.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = $"Admin")]
public class CustomerController : Controller
{
    private ICustomerService _customerService;
    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var response = await _customerService.GetAsync();
        if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
        {
            return View(new CustomerListViewModel(response.Data!));
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
        var response = await _customerService.GetByIdAsync(id);
        if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
        {
            response.Data!.PasswordConfirm = response.Data.Password;
            return View(response.Data);
        }
        return View("Error", response.Description);
    }

    [HttpPost]
    public async Task<IActionResult> Save(Customer item)
    {
        if (ModelState.IsValid)
        {
            // Create.
            if (item.Id == 0)
            {
                var response = await _customerService.CreateAsync(item);
                if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", response.Description);
            }
            // Update.
            else
            {
                var response = await _customerService.UpdateAsync(item);
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
        var response = await _customerService.DeleteAsync(id);
        if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
        {
            return RedirectToAction("Index");
        }
        return View("Error", response.Description);
    }
}
