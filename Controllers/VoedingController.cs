using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Controllers
{
    public class VoedingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
