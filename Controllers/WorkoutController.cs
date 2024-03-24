using FitnessTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleManager.Ui.Mvc.Core;
using System;

namespace FitnessTracker.Controllers;

public class WorkoutController : Controller
{
    private readonly FitnessTrackerDbContext _context;

    public WorkoutController(FitnessTrackerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(new WorkoutPage(await _context.Workouts.ToListAsync()));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string action, Workout workout)
    {
        if (action == "GenerateWorkout")
        {
            _context.Workouts.Add(await new RandomWorkout()._RandomWorkout(workout));
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        if (action == "GenerateSession")
        {
            for (var i = 0; i < 5; i++)
            {
                _context.Workouts.Add(await new RandomWorkout()._RandomWorkout(workout));
            }
            await _context.SaveChangesAsync();
        }
        else if (action == "Reset")
        {
            _context.Workouts.RemoveRange(_context.Workouts);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index");
    }
}