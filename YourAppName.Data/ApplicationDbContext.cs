using Microsoft.EntityFrameworkCore;
using YourAppName.Data.Models; // Assuming models are in this namespace

namespace YourAppName.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ProductOrService> ProductsOrServices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the composite key for OrderItem (junction table)
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.ProductOrServiceId });

            // Configure the many-to-many relationship between Order and ProductOrService through OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.ProductOrService)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductOrServiceId);

            // Configure one-to-many relationship between Client and Order
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Client)
                .HasForeignKey(o => o.ClientId);

            // Configure one-to-many relationship between Employee and Order (optional Employee)
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Orders)
                .WithOne(o => o.Employee)
                .HasForeignKey(o => o.EmployeeId)
                .IsRequired(false); // EmployeeId is nullable in Order

            // Configure one-to-one relationship between User and Employee
            // Assuming an Employee can have one User account, and a User is linked to one Employee
            // The foreign key 'UserId' is in the Employee table.
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithOne(u => u.Employee)
                .HasForeignKey<Employee>(e => e.UserId)
                .IsRequired(false); // UserId is nullable in Employee

            // Set precision for decimal properties if necessary (example for ProductOrService.Price and Order.TotalAmount)
            modelBuilder.Entity<ProductOrService>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.PriceAtTimeOfOrder)
                .HasColumnType("decimal(18,2)");
        }
    }
}
