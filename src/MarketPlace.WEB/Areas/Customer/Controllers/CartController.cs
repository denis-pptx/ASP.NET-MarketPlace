namespace MarketPlace.WEB.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize(Roles = "Customer")]
public class CartController : Controller
{
    private readonly ICartService _cartService;
    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _cartService.GetAsync(User.Identity?.Name ?? "");
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return View(response.Data!);
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
    }

    public async Task<IActionResult> Add(int productId)
    {
        var response = await _cartService.AddProductAsync(User.Identity?.Name ?? "", productId);
        return StatusCode((int)response.StatusCode, response.Description);
    }

    public async Task<IActionResult> Remove(int productId)
    {
        var response = await _cartService.RemoveProductAsync(User.Identity?.Name ?? "", productId);
        return StatusCode((int)response.StatusCode, response.Description);
    }
}
