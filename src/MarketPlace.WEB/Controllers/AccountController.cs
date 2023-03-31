using MarketPlace.BLL.Interfaces;
using MarketPlace.BLL.ViewModels;
using MarketPlace.BLL.Infrastracture;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;


namespace MarketPlace.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult AccessDenied() => StatusCode(403, "Forbidden");

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.LoginAsync(vm);
                if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
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
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.RegisterAsync(vm);
                if (response.StatusCode == BLL.Infrastracture.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(response.Data!);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", response.Description);
            }

            return View(vm);
        }
    }
}
