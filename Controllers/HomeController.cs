using System.Diagnostics;
using System.Security.Claims;
using FitnessTracker.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Ui.Mvc.Core;

namespace FitnessTracker.Controllers;

public class HomeController : Controller
{
    private readonly FitnessTrackerDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, FitnessTrackerDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(User user)
    {
        var validUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);

        if (validUser != null && validUser.Password == user.Password)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, validUser.Email)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Workout");
        }

        ModelState.AddModelError("", "Invalid email or password");
        return View("Index", user);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
