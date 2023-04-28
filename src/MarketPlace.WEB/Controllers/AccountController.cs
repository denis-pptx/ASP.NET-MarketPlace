using MarketPlace.BLL.DTO;

namespace MarketPlace.WEB.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }


    [HttpGet]
    public IActionResult AccessDenied() => StatusCode(403, "Forbidden");

    [HttpGet]
    public IActionResult UnauthorizedModal() => PartialView("_Unauthorized");

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }


    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginDTO vm)
    {
        if (ModelState.IsValid)
        {
            var response = await _accountService.LoginAsync(vm);
            if (response.StatusCode == HttpStatusCode.OK)        
            {
                await HttpContext.SignInAsync(response.Data!);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", response.Description);
        }

        return View(vm);
    }


    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDTO dto)
    {
        if (ModelState.IsValid)
        {
            var response = await _accountService.RegisterAsync(dto);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                await HttpContext.SignInAsync(response.Data!);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", response.Description);
        }

        return View(dto);
    }
}
