using FitnessTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
	    var recipeAPI = new VoedingAPI();
	    var fetchedRecipes = await recipeAPI.GetRecipes(voeding, 6); // Fetch 5 recipes

	    // Pass the fetched recipes to the VoedingPage constructor
	    var voedingPage = new VoedingPage(fetchedRecipes);
	    return View("Index", voedingPage);
    }
    public async Task<IActionResult> Details(int id)
    {
	    var recipeAPI = new VoedingAPI();

	    var recipeInformation = await recipeAPI.GetRecipeInformation(id);

	    if (recipeInformation == null)
	    {
		    return NotFound();
	    }

	    return View(recipeInformation);
    }



}