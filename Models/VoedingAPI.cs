using Newtonsoft.Json;

namespace FitnessTracker.Models;

public class VoedingAPI
{
	public async Task<List<Voeding>> GetRecipes(Voeding voeding, int numberOfRecipes)
	{
		using var client = new HttpClient();
		var requestUri = "https://api.spoonacular.com/recipes/complexSearch?$";
		requestUri += voeding.query != null ? $"&query={voeding.query}" : "";
		requestUri += voeding.intolerance != null ? $"&intolerances={voeding.intolerance}" : "";
		requestUri += voeding.cuisine != null ? $"&cuisine={voeding.cuisine}" : "";
		requestUri += voeding.diet != null ? $"&diet={voeding.diet}" : "";
		requestUri += $"&number={numberOfRecipes}&apiKey=568e8dd7bb494bae980d7a87b9979867";

		var response = await client.GetAsync(requestUri);

		var jsonString = await response.Content.ReadAsStringAsync();
		var model = JsonConvert.DeserializeObject<RootVoeding>(jsonString);
		return model.results;
	}
	public async Task<Voeding> GetRecipeInformation(int id)
	{
		using var client = new HttpClient();
		var requestUri = $"https://api.spoonacular.com/recipes/{id}/information?apiKey=568e8dd7bb494bae980d7a87b9979867";

		var response = await client.GetAsync(requestUri);

		if (response.IsSuccessStatusCode)
		{
			var jsonString = await response.Content.ReadAsStringAsync();
			var model = JsonConvert.DeserializeObject<Voeding>(jsonString);
			return model;
		}
		else
		{
			// Handle error response
			return null;
		}
	}
}

public class RootVoeding
{
	public List<Voeding> results { get; set; }
}