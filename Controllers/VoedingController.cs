
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

<<<<<<< Updated upstream
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

	    var isFavorite = _context.FavoriteRecipes.Any(fr =>fr.RecipeId == id);
	    ViewBag.IsFavorite = (bool?)isFavorite;

	    return View(recipeInformation);
    }

    public IActionResult Favorites()
    {
	    var favoriteRecipeIds = _context.FavoriteRecipes.Select(fr => fr.RecipeId).ToList();

	    var favoriteRecipes = _context.recipes.Where(r => favoriteRecipeIds.Contains(r.Id)).ToList();

	    return View("Favorites");
    }
    [HttpPost]
    public async Task<IActionResult> AddToFavorites(int? recipeId)
    {
	    if (recipeId == null)
	    {
		    return BadRequest("Recipe ID cannot be null.");
	    }

	    var id = recipeId.GetValueOrDefault();
	    _context.FavoriteRecipes.Add(new FavoriteRecipe { RecipeId = id });
	    await _context.SaveChangesAsync();

	    return RedirectToAction("Details", new { id });
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromFavorites(int? recipeId)
    {
	    if (recipeId == null)
	    {
		    return BadRequest("Recipe ID cannot be null.");
	    }

	    var id = recipeId.GetValueOrDefault();
	    var favoriteRecipe = _context.FavoriteRecipes.FirstOrDefault(fr => fr.RecipeId == id);
	    if (favoriteRecipe == null)
	    {
		    return NotFound();
	    }

	    _context.FavoriteRecipes.Remove(favoriteRecipe);
	    await _context.SaveChangesAsync();

	    return RedirectToAction("Details", new { id });
    }

}
=======
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Search(Voeding voeding)
	{
		var recipeAPI = new VoedingAPI();
		var fetchedRecipes = await recipeAPI.GetRecipes(voeding, 6);

		var voedingPage = new VoedingPage(fetchedRecipes);
		return View("Index", voedingPage);
	}

	public async Task<IActionResult> Details(int id)
	{
		var recipeAPI = new VoedingAPI();
		var recipeInformation = await recipeAPI.GetRecipeInformation(id);

		if (recipeInformation == null) return NotFound();

		var isFavorite = _context.FavoriteRecipes.Any(fr => fr.RecipeId == id);
		ViewBag.IsFavorite = (bool?)isFavorite;

		return View(recipeInformation);
	}

	public IActionResult Favorites()
	{
		var favoriteRecipeIds = _context.FavoriteRecipes.Select(fr => fr.RecipeId).ToList();
		var voedingAPI = new VoedingAPI();
		var favoriteRecipes = favoriteRecipeIds.Select(recipeID => voedingAPI.GetRecipeInformation(recipeID).Result).ToList();
		return View(favoriteRecipes);
	}

	[HttpPost]
	public async Task<IActionResult> AddToFavorites(int? recipeId)
	{
		if (recipeId == null) return BadRequest("Recipe ID cannot be null.");

		var id = recipeId.GetValueOrDefault();
		_context.FavoriteRecipes.Add(new FavoriteRecipe { RecipeId = id });
		await _context.SaveChangesAsync();

		return RedirectToAction("Details", new { id });
	}

	[HttpPost]
	public async Task<IActionResult> RemoveFromFavorites(int? recipeId)
	{
		if (recipeId == null) return BadRequest("Recipe ID cannot be null.");

		var id = recipeId.GetValueOrDefault();
		var favoriteRecipe = _context.FavoriteRecipes.FirstOrDefault(fr => fr.RecipeId == id);
		if (favoriteRecipe == null) return NotFound();

		_context.FavoriteRecipes.Remove(favoriteRecipe);
		await _context.SaveChangesAsync();

		return RedirectToAction("Details", new { id });
	}
}
>>>>>>> Stashed changes
