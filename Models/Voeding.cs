namespace FitnessTracker.Models;

public class Voeding
{
	public int? Id { get; set; }
	public string? Title { get; set; }
	public string? Image { get; set; }
	public int? ReadyInMinutes { get; set; }
	public int? Servings { get; set; }
	public string? SourceUrl { get; set; }
	public string? SourceName { get; set; }
	public string? diet { get; set; }
	public string? cuisine { get; set; }
	public string? intolerance { get; set; }
	public string? query { get; set; }
	public List<string>? Ingredients { get; set; } // Ingredients for recipe details
	public string? Calories { get; set; }
	public string? Fat { get; set; }
	public string? Carbohydrates { get; set; }
	public string? Protein { get; set; }
	public string Description { get; set; }
	public string Instructions { get; set; }
}