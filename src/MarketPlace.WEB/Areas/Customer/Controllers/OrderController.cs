namespace MarketPlace.WEB.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize(Roles = "Customer")]
public class OrderController : Controller
{
    public IActionResult Index(IEnumerable<int> cartItems)
    {
        return View();
    }
}
