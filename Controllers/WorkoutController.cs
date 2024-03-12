using System.Diagnostics.CodeAnalysis;
using FitnessTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleManager.Ui.Mvc.Core;

namespace FitnessTracker.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly FitnessTrackerDbContext _context;
        public WorkoutController(FitnessTrackerDbContext context) { _context = context; }

        public IActionResult Index()
        {
            return View(_context.Workouts.ToList());
        }
        [HttpPost] [ValidateAntiForgeryToken]
        public async Task<Workout> LoadWorkout(Workout workout)
        {
            RandomWorkout randomWorkout = new RandomWorkout();
            return await randomWorkout._RandomWorkout(workout);
        }
    }
}
