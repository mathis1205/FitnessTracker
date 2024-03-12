using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [EmailAddress] public string Email { get; set; }
    [PasswordPropertyText] public string Password { get; set; }
}