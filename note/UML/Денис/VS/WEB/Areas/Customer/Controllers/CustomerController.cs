namespace MarketPlace.WEB.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize(Roles = "Customer")]
public class CustomerController : Controller
{
    private readonly ICustomerService _customerService;
    private readonly IAccountService _accountService;
    public CustomerController(ICustomerService customerService, IAccountService accountService)
    {
        _customerService = customerService;
        _accountService = accountService;
    }


    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        var response = await _customerService.GetByLoginAsync(User.Identity?.Name ?? "");
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return View(response.Data);
        }
        return View("Error", new ErrorViewModel(response.Deconstruct()));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(DAL.Entities.Customer item)
    {
        ModelState.Remove("PasswordConfirm");
        if (ModelState.IsValid)
        {
            var updateResponse = await _customerService.UpdateAsync(item);
            if (updateResponse.StatusCode == HttpStatusCode.OK)
            {
                await HttpContext.SignOutAsync();
                var loginResponse = await _accountService.LoginAsync(new LoginDTO()
                {
                    Login = item.Login,
                    Password = item.Password
                });

                if (loginResponse.StatusCode == HttpStatusCode.OK)
                {
                    await HttpContext.SignInAsync(loginResponse.Data!);
                    return RedirectToAction("Edit");
                }

                return View("Error", new ErrorViewModel(loginResponse.Deconstruct()));
            }
            ModelState.AddModelError("", updateResponse.Description);
        }
        return View(item);
    }
}
