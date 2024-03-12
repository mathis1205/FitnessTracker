using System.Web;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FitnessTracker.Models;

public class RandomWorkout
{
    public async Task<Workout?> _RandomWorkout(Workout oWorkout)
    {
        using var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://work-out-api1.p.rapidapi.com/search?Muscles={oWorkout.Muscles}"),
            Headers =
            {
                { "X-RapidAPI-Key", "d6aabef542mshb1bd46856c02514p1c98bfjsnfab2282b2958" },
                { "X-RapidAPI-Host", "work-out-api1.p.rapidapi.com" }
            }
        };
        var response = await client.SendAsync(request);
        if (!response.IsSuccessStatusCode) return null;
        var jsonString = await response.Content.ReadAsStringAsync();
        var myDeserializedClassList = JsonConvert.DeserializeObject<List<Root>>(jsonString);
        var myDeserializedClass = myDeserializedClassList[new Random().Next(0, myDeserializedClassList.Count)];
        var model = new Workout
        {
            Muscles = myDeserializedClass.Muscles,
            WorkOut = myDeserializedClass.WorkOut,
            Equipments = myDeserializedClass.Equipment,
            Explaination = myDeserializedClass.Explaination,
            Video = await GetVideo(myDeserializedClass.Video)
        };
        switch (myDeserializedClass.Intensity_Level)
        {
            case "Beginner":
                model.Beginner_Sets = myDeserializedClass.BeginnerSets;
                break;
            case "Intermediate":
                model.Intermediate_Sets = myDeserializedClass.IntermediateSets;
                break;
            case "Expert":
                model.Expert_Sets = myDeserializedClass.ExpertSets;
                break;
        }

        return model;
    }

    private async Task<string> GetVideo(string url)
    {
        var chromeOptions = new ChromeOptions();
        chromeOptions.AddArgument("--headless");
        chromeOptions.AddArgument("--disable-gpu");
        chromeOptions.AddArgument("--no-sandbox");
        var driver = new ChromeDriver(chromeOptions);

        try
        {
            driver.Navigate().GoToUrl(url);
            var firstVideoLink = driver.FindElement(By.XPath("//a[@id='video-title']"));
            var videoUrl = firstVideoLink.GetAttribute("href");
            return GetEmbedUrl(videoUrl);
        }
        finally
        {
            driver.Quit();
        }
    }

    private static string GetEmbedUrl(string fullYoutubeUrl)
    {
        var uri = new Uri(fullYoutubeUrl);
        var videoId = HttpUtility.ParseQueryString(uri.Query).Get("v");
        return "https://www.youtube.com/embed/" + videoId;
    }
}

public class Root
{
    public string Muscles { get; set; }
    public string WorkOut { get; set; }
    public string Intensity_Level { get; set; }

    [JsonProperty("Beginner Sets")] public string BeginnerSets { get; set; }

    [JsonProperty("Intermediate Sets")] public string IntermediateSets { get; set; }

    [JsonProperty("Expert Sets")] public string ExpertSets { get; set; }

    public string Equipment { get; set; }
    public string Explaination { get; set; }

    [JsonProperty("Long Explanation")] public string LongExplanation { get; set; }

    public string Video { get; set; }
}