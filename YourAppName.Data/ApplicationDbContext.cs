using Microsoft.AspNetCore.Identity; // Required for IdentityRole
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Required for IdentityDbContext
using Microsoft.EntityFrameworkCore;
using YourAppName.Data.Models;

namespace YourAppName.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int> // Updated inheritance
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ProductOrService> ProductsOrServices { get; set; }
        public DbSet<Order> Orders { get; set; }
        // public DbSet<User> Users { get; set; } // This is now provided by IdentityDbContext as Users
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // IMPORTANT: Call base.OnModelCreating for Identity setup

            // Configure the composite key for OrderItem (junction table)
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.ProductOrServiceId });

            // ... (rest of your existing OnModelCreating configurations for OrderItem, Client, Employee, Order, ProductOrService)
            // IMPORTANT: Identity tables (AspNetUsers, AspNetRoles, etc.) are configured by base.OnModelCreating.
            // You might need to adjust your User table name if it defaults to AspNetUsers and you want "Users"
            // For now, assume default table names (e.g., AspNetUsers for User).

             modelBuilder.Entity<OrderItem>()
                 .HasOne(oi => oi.Order)
                 .WithMany(o => o.OrderItems)
                 .HasForeignKey(oi => oi.OrderId);

             modelBuilder.Entity<OrderItem>()
                 .HasOne(oi => oi.ProductOrService)
                 .WithMany(p => p.OrderItems)
                 .HasForeignKey(oi => oi.ProductOrServiceId);

             modelBuilder.Entity<Client>()
                 .HasMany(c => c.Orders)
                 .WithOne(o => o.Client)
                 .HasForeignKey(o => o.ClientId);

             modelBuilder.Entity<Employee>()
                 .HasMany(e => e.Orders)
                 .WithOne(o => o.Employee)
                 .HasForeignKey(o => o.EmployeeId)
                 .IsRequired(false);

             // User to Employee relationship (Employee has a nullable UserId, User has an Employee navigation)
             // IdentityUser (our User class) is the principal here. Employee is dependent.
             modelBuilder.Entity<User>()
                 .HasOne(u => u.Employee)
                 .WithOne(e => e.User)
                 .HasForeignKey<Employee>(e => e.UserId) // Foreign key is in Employee table
                 .IsRequired(false);


             modelBuilder.Entity<ProductOrService>()
                 .Property(p => p.Price)
                 .HasColumnType("decimal(18,2)");

             modelBuilder.Entity<Order>()
                 .Property(o => o.TotalAmount)
                 .HasColumnType("decimal(18,2)");

             modelBuilder.Entity<OrderItem>()
                 .Property(oi => oi.PriceAtTimeOfOrder)
                 .HasColumnType("decimal(18,2)");

             // If you want to ensure your User table is named "Users" not "AspNetUsers"
             // and Role table is "Roles" not "AspNetRoles" etc.
             modelBuilder.Entity<User>(b =>
             {
                 b.ToTable("Users"); // Override default AspNetUsers
             });

             modelBuilder.Entity<IdentityRole<int>>(b =>
             {
                 b.ToTable("Roles"); // Override default AspNetRoles
             });
              modelBuilder.Entity<IdentityUserClaim<int>>(b =>
             {
                 b.ToTable("UserClaims");
             });

             modelBuilder.Entity<IdentityUserRole<int>>(b =>
             {
                 b.ToTable("UserRoles");
             });

             modelBuilder.Entity<IdentityUserLogin<int>>(b =>
             {
                 b.ToTable("UserLogins");
             });

             modelBuilder.Entity<IdentityRoleClaim<int>>(b =>
             {
                 b.ToTable("RoleClaims");
             });

             modelBuilder.Entity<IdentityUserToken<int>>(b =>
             {
                 b.ToTable("UserTokens");
             });
        }
    }
}
