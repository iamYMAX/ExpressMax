using System.Collections.Generic; // Required for ICollection

namespace YourAppName.Data.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ContactPerson { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        // Navigation property for related Orders
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
