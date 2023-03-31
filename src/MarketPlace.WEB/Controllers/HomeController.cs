using MarketPlace.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.WEB.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}
