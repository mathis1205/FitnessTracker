using FitnessTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Controllers
{
    public class WorkoutController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Workout workout)
        {
            return View(LoadWorkouts(workout).Result);
        }

        public async Task<Workout> LoadWorkouts(Workout workout)
        {
            RandomWorkout randomWorkout = new RandomWorkout();
            return await randomWorkout._RandomWorkout(workout);
        }
    }
}
