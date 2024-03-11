namespace FitnessTracker.Models;

public class Workout
{
    public int Id { get; set; }
    public required string Muscles { get; set; }
    public string? WorkOut { get; set; }
    public string? Intensity_Level { get; set; }
    public string? Beginner_Sets { get; set; }
    public string? Intermediate_Sets { get; set; }
    public string? Expert_Sets { get; set; }
    public string? Equipments { get; set; }
    public string? Explaination { get; set; }
    public string? Video { get; set; }
}