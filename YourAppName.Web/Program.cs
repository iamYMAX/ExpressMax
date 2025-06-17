using Microsoft.AspNetCore.Identity; // Required for AddIdentity, IdentityRole
using Microsoft.EntityFrameworkCore;
using YourAppName.Data;
using YourAppName.Data.Models; // Required for User class

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// Configure ASP.NET Core Identity
builder.Services.AddIdentity<User, IdentityRole<int>>(options => {
    // Configure identity options here if needed (e.g., password requirements)
    options.SignIn.RequireConfirmedAccount = false; // Or true, depending on requirements
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6; // Example: Set password length
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders(); // For password reset, email confirmation tokens

// Configure cookie settings (optional, but common)
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; // Path to your login page
    options.LogoutPath = "/Account/Logout"; // Path to your logout action
    options.AccessDeniedPath = "/Account/AccessDenied"; // Path for access denied
    options.SlidingExpiration = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Add Authentication middleware
app.UseAuthorization();  // Add Authorization middleware

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
