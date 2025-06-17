using System;
using System.Collections.Generic; // Required for ICollection (for OrderItems later)

namespace YourAppName.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int ClientId { get; set; } // Foreign key for Client
        public virtual Client Client { get; set; } // Navigation property for Client

        public int? EmployeeId { get; set; } // Foreign key for Employee (nullable)
        public virtual Employee? Employee { get; set; } // Navigation property for Employee

        public string? Status { get; set; }
        public decimal TotalAmount { get; set; }

        // Navigation property for OrderItems (for many-to-many with ProductOrService)
        // This will be fully utilized when OrderItem model is created
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
