using FitnessTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace PeopleManager.Ui.Mvc.Core;

public class FitnessTrackerDbContext(DbContextOptions<FitnessTrackerDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Workout> Workouts => Set<Workout>();

    public void Seed()
    {
        var people = new List<User>
        {
            new() { FirstName = "John", LastName = "Doe", Email = "a@a", Password = "a" }
        };
        Users.AddRange(people);
        SaveChanges();
    }
}