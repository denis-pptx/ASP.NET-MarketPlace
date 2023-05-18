namespace MarketPlace.WEB.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize(Roles = "Customer")]
public class PaymentController : Controller
{
    private readonly ICartService _cartService;
    private readonly IOrderService _orderService;

    public PaymentController(ICartService cartService, IOrderService orderService)
    {
        _cartService = cartService;
        _orderService = orderService;
    }

    public async Task<IActionResult> Buy(IEnumerable<int> cartItemsIds)
    {
        var orderResponse = await _orderService.AddAsync(cartItemsIds);
        if (orderResponse.StatusCode== HttpStatusCode.OK)
        {
            var cartResponse = await _cartService.RemoveAsync(cartItemsIds);
            if (cartResponse.StatusCode== HttpStatusCode.OK)
            {
                return RedirectToAction("Thanks");
            }
            return View("Error", new ErrorViewModel(orderResponse.Deconstruct()));
        }
        return View("Error", new ErrorViewModel(orderResponse.Deconstruct()));
    }

    public IActionResult Thanks()
    {
        return View();
    }
}
