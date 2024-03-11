using FitnessTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace PeopleManager.Ui.Mvc.Core;

public class PeopleManagerDbContext(DbContextOptions<PeopleManagerDbContext> options) : DbContext(options)
{
    public DbSet<Person> People => Set<Person>();
    public DbSet<Workout> Workouts => Set<Workout>();

    public void Seed()
    {
        var workout = new List<Workout>
        {
            new() { Muscles = "Biceps", Explaination = "excpl" }
        };
        Workouts.AddRange(workout);
        var people = new List<Person>
        {
            new() { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Password = "John" }
        };
        People.AddRange(people);

        SaveChanges();
    }
}