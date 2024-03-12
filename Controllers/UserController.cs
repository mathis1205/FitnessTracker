using FitnessTracker.Models;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Ui.Mvc.Core;

namespace FitnessTracker.Controllers;

public class UserController : Controller
{
    private readonly FitnessTrackerDbContext _dbContext;

    public UserController(FitnessTrackerDbContext context)
    {
        _dbContext = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        return RedirectToAction("Index", "Home");
    }
}