using FitnessTracker.Models;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Ui.Mvc.Core;

namespace FitnessTracker.Controllers
{
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
}
