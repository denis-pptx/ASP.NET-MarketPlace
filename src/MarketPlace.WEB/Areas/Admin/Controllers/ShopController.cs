using MarketPlace.WEB.Areas.Admin.Models;

namespace MarketPlace.WEB.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ShopController : Controller
{
    private IShopService _shopService;
    public ShopController(IShopService shopService)
    {
        _shopService = shopService;
    }


    public async Task<IActionResult> Index(string? name)
    {
        var response = await _shopService.GetBySimilarNameAsync(name);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return View(new ShopListViewModel()
            {
                Shops = response.Data!.OrderBy(s => s.Name),
                Name = name
            });
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var response = await _shopService.GetByIdAsync(id);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return View(response.Data);
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
    }

    [HttpGet]
    public async Task<IActionResult> Save(int id)
    {
        // Create.
        if (id == 0)
        {
            return View(new Shop());
        }

        // Update.
        var response = await _shopService.GetByIdAsync(id);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return View(response.Data);
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save([Bind("Id,Name,Description")] Shop item)
    {
        if (ModelState.IsValid)
        {
            // Create.
            if (item.Id == 0)
            {
                var response = await _shopService.CreateAsync(item);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", response.Description);
            }
            // Update.
            else
            {
                var response = await _shopService.UpdateAsync(item);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", response.Description);
            }

        }
        return View("Save", item);
    }


    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _shopService.GetByIdAsync(id);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return View(response.Data);
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var response = await _shopService.DeleteAsync(id);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return RedirectToAction("Index");
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
    }
}
