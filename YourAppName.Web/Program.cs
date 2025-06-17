using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YourAppName.Data;
using YourAppName.Data.Models; // Required for User class

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddIdentity<User, IdentityRole<int>>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
});

var app = builder.Build();

// Seed the database with initial user and role
// Scope this to avoid issues during EF migrations design-time tool runs
// Also, typically done only once or in Development
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
        // Pass logger factory or logger for SeedDataAsync to use if needed for its internal errors
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        await SeedDataAsync(userManager, roleManager, loggerFactory.CreateLogger("SeedData")); // Call the seeding method
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
        // Depending on policy, you might want to throw or handle this gracefully
    }
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


// Seeding method (can be in this file or a separate static class)
async Task SeedDataAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, ILogger logger)
{
    string adminRoleName = "Admin";
    string adminEmail = "admin@example.com"; // Or any desired admin email/username
    string adminPassword = "Password123!"; // Choose a strong password, this is just an example

    // Ensure Admin role exists
    if (await roleManager.FindByNameAsync(adminRoleName) == null)
    {
        await roleManager.CreateAsync(new IdentityRole<int>(adminRoleName));
        logger.LogInformation($"Role '{adminRoleName}' created.");
    }
    else
    {
        logger.LogInformation($"Role '{adminRoleName}' already exists.");
    }

    // Ensure Admin user exists
    if (await userManager.FindByNameAsync(adminEmail) == null)
    {
        User adminUser = new User
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true, // Optional: confirm email immediately for seeded admin
            Role = adminRoleName // Custom Role property, if still used
        };
        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (result.Succeeded)
        {
            logger.LogInformation($"User '{adminEmail}' created successfully.");
            await userManager.AddToRoleAsync(adminUser, adminRoleName);
            logger.LogInformation($"User '{adminEmail}' added to role '{adminRoleName}'.");
        }
        else
        {
            foreach(var error in result.Errors)
            {
                logger.LogError($"Error creating admin user '{adminEmail}': {error.Code} - {error.Description}");
            }
        }
    }
    else
    {
        logger.LogInformation($"User '{adminEmail}' already exists.");
    }
}
