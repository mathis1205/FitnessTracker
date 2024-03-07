using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Controllers
{
    public class WorkoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
