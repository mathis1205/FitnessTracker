using FitnessTracker.Models;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Ui.Mvc.Core;

namespace PeopleManager.Ui.Mvc.Controllers;

public class PeopleController : Controller
{
    private readonly PeopleManagerDbContext _dbContext;

    public PeopleController(PeopleManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
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
    public IActionResult Create(Person person)
    {
        _dbContext.People.Add(person);
        _dbContext.SaveChanges();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var person = _dbContext.People.FirstOrDefault(p => p.Id == id);
        if (person == null) return RedirectToAction("Index");
        return View(person);
    }

    [HttpPost]
    public IActionResult Edit(Person person)
    {
        if (string.IsNullOrWhiteSpace(person.FirstName) || string.IsNullOrWhiteSpace(person.LastName))
            return RedirectToAction("Index");
        _dbContext.People.Update(person);
        _dbContext.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete([FromRoute] int id)
    {
        var person = _dbContext.People
            .FirstOrDefault(p => p.Id == id);

        return View(person);
    }

    [HttpPost("/[controller]/Delete/{id:int}")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed([FromRoute] int id)
    {
        var person = _dbContext.People
            .FirstOrDefault(p => p.Id == id);

        if (person is null) return RedirectToAction("Index");

        _dbContext.People.Remove(person);
        _dbContext.SaveChanges();

        return RedirectToAction("Index");
    }
}