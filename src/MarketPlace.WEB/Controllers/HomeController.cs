using MarketPlace.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.WEB.Controllers;

public class HomeController : Controller
{
    private IUnitOfWork _unitOfWork;
    public HomeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        return View();
    }
}
