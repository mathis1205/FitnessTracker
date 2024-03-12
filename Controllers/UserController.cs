using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Controllers;

public class UserController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }
}