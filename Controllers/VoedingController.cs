using FitnessTracker.Models;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Ui.Mvc.Core;

namespace FitnessTracker.Controllers;

public class VoedingController : Controller
{
    private readonly FitnessTrackerDbContext _context;

    public VoedingController(FitnessTrackerDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(new VoedingPage(_context.recipes.ToList()));
    }

    [HttpGet]
    public IActionResult Search()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Search(Voeding voeding)
    {
        var recipe = new VoedingAPI();
        voeding = await recipe.GetRecipe(voeding);

        voeding.ImageUrl = "<ImageUrl>";
        voeding.SourceName = "<SourceName>";
        voeding.SourceUrl = "<SourceUrl>";
        voeding.Title = "<Title>";
        voeding.query = "<query>";
        voeding.intolerance = "<intolerance>";
        voeding.cuisine = "<cuisine>";
        voeding.diet = "<diet>";

        _context.recipes.Add(voeding);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}