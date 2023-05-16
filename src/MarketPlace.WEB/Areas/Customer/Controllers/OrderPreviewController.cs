namespace MarketPlace.WEB.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize(Roles = "Customer")]
public class OrderPreviewController : Controller
{
    private readonly ICartService _cartService;
    public OrderPreviewController(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<IActionResult> Index(IEnumerable<int> cartItemsIds)
    {
        var response = await _cartService.GetAsync(cartItemsIds);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return View(response.Data);
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
    }
}
