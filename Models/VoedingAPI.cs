using Newtonsoft.Json;

namespace FitnessTracker.Models;

public class VoedingAPI
{
    public async Task<Voeding> GetRecipe(Voeding voeding)
    {
        using var client = new HttpClient();
        var requestUri = "https://api.spoonacular.com/recipes/complexSearch?$";
        requestUri += voeding.query != null ? $"&query={voeding.query}" : "";
        requestUri += voeding.intolerance != null ? $"&intolerances={voeding.intolerance}" : "";
        requestUri += voeding.cuisine != null ? $"&cuisine={voeding.cuisine}" : "";
        requestUri += voeding.diet != null ? $"&diet={voeding.diet}" : "";
        requestUri += "&apiKey=568e8dd7bb494bae980d7a87b9979867";

        var response = await client.GetAsync(requestUri);

        var jsonString = await response.Content.ReadAsStringAsync();
        var model = JsonConvert.DeserializeObject<RootVoeding>(jsonString);
        var recipes = model.results;
        var _voeding = recipes.FirstOrDefault();
        return _voeding;
    }
}

public class RootVoeding
{
    public List<Voeding> results { get; set; }
}