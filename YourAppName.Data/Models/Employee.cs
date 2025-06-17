using System.Collections.Generic; // Required for ICollection

namespace YourAppName.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Position { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public int? UserId { get; set; } // Foreign key for User (nullable for one-to-one)
        public virtual User? User { get; set; } // Navigation property for User

        // Navigation property for related Orders
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
