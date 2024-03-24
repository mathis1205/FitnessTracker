using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Newtonsoft.Json;

namespace FitnessTracker.Models;

public class RandomWorkout
{
    private const string youtubeApiKey = "AIzaSyBQ1G_Kei2BSW3veNJaWpWyiRLTD7ORqNk";

    public async Task<Workout?> _RandomWorkout(Workout oWorkout)
    {
        var httpClient = new HttpClient();
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

        var response = await httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode) return null;

        var jsonString = await response.Content.ReadAsStringAsync();
        var myDeserializedClassList = JsonConvert.DeserializeObject<List<Root>>(jsonString);

        var random = new Random();
        var randomWorkout = myDeserializedClassList[random.Next(0, myDeserializedClassList.Count)];

        var youtubeService = new YouTubeService(new BaseClientService.Initializer
        {
            ApiKey = youtubeApiKey,
            ApplicationName = "Fitness Tracker"
        });

        var searchListRequest = youtubeService.Search.List("snippet");
        searchListRequest.Q = $"{randomWorkout.WorkOut} workout";
        searchListRequest.MaxResults = 1;

        var searchListResponse = await searchListRequest.ExecuteAsync();
        var videoId = searchListResponse.Items.FirstOrDefault()?.Id.VideoId;
        if (videoId == null) return null;

        var model = new Workout
        {
            Muscles = randomWorkout.Muscles,
            WorkOut = randomWorkout.WorkOut,
            Equipments = randomWorkout.Equipment,
            Explaination = randomWorkout.Explaination,
            Video = $"https://www.youtube.com/embed/{videoId}"
        };

        switch (randomWorkout.Intensity_Level)
        {
            case "Beginner":
                model.Beginner_Sets = randomWorkout.BeginnerSets;
                break;
            case "Intermediate":
                model.Intermediate_Sets = randomWorkout.IntermediateSets;
                break;
            case "Expert":
                model.Expert_Sets = randomWorkout.ExpertSets;
                break;
        }

        return model;
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