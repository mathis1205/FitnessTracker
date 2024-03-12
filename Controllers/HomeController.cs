using FitnessTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleManager.Ui.Mvc.Core;
using System.Diagnostics;

namespace FitnessTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly FitnessTrackerDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, FitnessTrackerDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            var validUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);

            if (validUser != null)
            {
                if (validUser.Password == user.Password)
                {
                    return RedirectToAction("Index", "Workout");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect password. Please try again.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Account not found. Please try again.");
            }
            return View("Index", user);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
