using FitnessTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace PeopleManager.Ui.Mvc.Core;

public class FitnessTrackerDbContext(DbContextOptions<FitnessTrackerDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Workout> Workouts => Set<Workout>();

    public void Seed()
    {
        var workout = new List<Workout>
        {
            new() { Muscles = "Biceps", Explaination = "excpl" ,Beginner_Sets = "3 sets", Equipments = "Bars", Video = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstley"}
        };
        Workouts.AddRange(workout);
        var people = new List<User>
        {
            new() { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Password = "John" },
            new() { FirstName = "John", LastName = "Doe", Email = "a@a", Password = "a" }
        };
        Users.AddRange(people);
        SaveChanges();
    }
}