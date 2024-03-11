using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Models
{
    public class Person
    {
        public int Id { get; set; }
        [DisplayName("First name")]
        public required string FirstName { get; set; }
        [DisplayName("Last name")]
        public required string LastName { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        [PasswordPropertyText]
        public required string Password { get; set; }
    }
}
