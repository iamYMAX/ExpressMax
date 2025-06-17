namespace YourAppName.Data.Models
{
    public class OrderItem
    {
        // Foreign key to Order
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        // Foreign key to ProductOrService
        public int ProductOrServiceId { get; set; }
        public virtual ProductOrService ProductOrService { get; set; }

        public int Quantity { get; set; }
        public decimal PriceAtTimeOfOrder { get; set; } // To record the price at the moment of purchase
    }
}
