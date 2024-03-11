using Newtonsoft.Json;

namespace FitnessTracker.Models;

public class RandomWorkout
{
    public async Task<Workout?> _RandomWorkout(Workout oWorkout)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync($"https://work-out-api1.p.rapidapi.com/search?Muscles={oWorkout.Muscles}");
        if (!response.IsSuccessStatusCode) return null;
        var jsonString = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<dynamic>(jsonString);
        var workout = data.workouts[0];
        var model = new Workout
        {
            Muscles = workout.Muscle,
            WorkOut = workout.Work_Out,
            Equipments = workout.Equipments,
            Explaination = workout.Explanation,
            Video = workout.Video
        };
        switch (workout.Intensity_Level)
        {
            case "Beginner":
                model.Beginner_Sets = workout.Sets;
                break;
            case "Intermediate":
                model.Intermediate_Sets = workout.Sets;
                break;
            case "Expert":
                model.Expert_Sets = workout.Sets;
                break;
        }
        return model;
    }
}