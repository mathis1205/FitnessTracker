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
                { "X-RapidAPI-Host", "work-out-api1.p.rapidapi.com" },
            },
        };
        var response = await client.SendAsync(request);
        if (!response.IsSuccessStatusCode) return null;
        var jsonString = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<dynamic>(jsonString);
        //var workout = data.workouts[0];
        var model = new Workout
        {
            Muscles = data.Muscle,
            WorkOut = data.Work_Out,
            Equipments = data.Equipments,
            Explaination = data.Explanation,
            Video = GetVideo(data.Video)
        };
        switch (data.Intensity_Level)
        {
            case "Beginner":
                model.Beginner_Sets = data.Sets;
                break;
            case "Intermediate":
                model.Intermediate_Sets = data.Sets;
                break;
            case "Expert":
                model.Expert_Sets = data.Sets;
                break;
        }
        return model;
    }

    private async Task<string> GetVideo(string url)
    {
        string searchQuery = "https://www.youtube.com/results?search_query=Barbell+Curl";

        var chromeOptions = new ChromeOptions();
        chromeOptions.AddArgument("--headless");
        chromeOptions.AddArgument("--disable-gpu");
        chromeOptions.AddArgument("--no-sandbox");
        var driver = new ChromeDriver(chromeOptions);

        try
        {
            driver.Navigate().GoToUrl(searchQuery);
            var firstVideoLink = driver.FindElement(By.XPath("//a[@id='video-title']"));
            string videoUrl = firstVideoLink.GetAttribute("href");
            return GetEmbedUrl(videoUrl);
        }
        finally
        {
            driver.Quit();
        }
    }
    private static string GetEmbedUrl(string fullYoutubeUrl)
    {
        Uri uri = new Uri(fullYoutubeUrl);
        string videoId = System.Web.HttpUtility.ParseQueryString(uri.Query).Get("v");
        return "https://www.youtube.com/embed/" + videoId;
    }
}