namespace MarketPlace.WEB.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize(Roles = "Customer")]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _orderService.GetAsync(User.Identity!.Name!);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return View(response.Data);
        }
        return View("Error", response.Deconstruct());
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _orderService.DeleteAsync(id);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return RedirectToAction("Index");
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
    }
}
