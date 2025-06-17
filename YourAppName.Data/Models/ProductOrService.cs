using System.Collections.Generic; // Required for ICollection

namespace YourAppName.Data.Models
{
    public class ProductOrService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsService { get; set; }

        // Navigation property for OrderItems (for many-to-many with Order)
        // This will be fully utilized when OrderItem model is created
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
