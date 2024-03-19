using FitnessTracker.Models;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Ui.Mvc.Core;

namespace FitnessTracker.Controllers;

public class WorkoutController : Controller
{
    private readonly FitnessTrackerDbContext _context;

    public WorkoutController(FitnessTrackerDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(new WorkoutPage(_context.Workouts.ToList()));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Workout workout)
    {
        var randomWorkout = new RandomWorkout();
        workout = await randomWorkout._RandomWorkout(workout);

        _context.Workouts.Add(workout);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}