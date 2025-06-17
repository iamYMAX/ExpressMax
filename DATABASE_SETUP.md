# Database Setup Instructions

This document explains how to create and initialize the application database using Entity Framework Core migrations.

## Prerequisites

1.  **.NET SDK:** Ensure you have the .NET SDK installed (version 6.0 or later, matching the project's target framework).
2.  **EF Core Tools:** The EF Core command-line tools must be installed. If you haven't installed them globally, you can do so by running:
    ```bash
    dotnet tool install --global dotnet-ef --version 7.0.0
    ```
    (Adjust version if needed, though 7.0.0 was used for EF packages. If you prefer project-local tools, ensure `Microsoft.EntityFrameworkCore.Tools` is referenced in one of your projects and use `dotnet ef` from there.)

## Applying Migrations

The database schema is defined by migrations located in the `YourAppName.Data/Migrations` folder. To create the database and apply these migrations, follow these steps:

1.  **Open your terminal or command prompt.**
2.  **Navigate to the root directory of the solution** (the directory containing `YourAppName.sln`, `YourAppName.Web`, and `YourAppName.Data` folders).
3.  **Run the following command:**

    ```bash
    dotnet ef database update --startup-project YourAppName.Web/YourAppName.Web.csproj --project YourAppName.Data/YourAppName.Data.csproj
    ```

    Alternatively, if your terminal is already inside the `YourAppName.Web` directory, you might be able to simplify it, but the command above is explicit and robust when run from the solution root.

## What This Command Does

-   **Checks for existing database:** It looks for a database based on the connection string in `YourAppName.Web/appsettings.json` (which is currently `Data Source=your_application.db` for SQLite).
-   **Creates database if not exists:** If the SQLite database file (`your_application.db`) doesn't exist in the output directory of `YourAppName.Web` (usually `YourAppName.Web/bin/Debug/net6.0/`), it will be created.
-   **Applies pending migrations:** It executes all migrations that haven't yet been applied to the database. This includes:
    -   `InitialCreate`: Sets up the core application tables (Clients, Orders, Products, etc.).
    -   `AddIdentitySchema`: Sets up the tables required by ASP.NET Core Identity (Users, Roles, Claims, etc.).
-   **Updates `__EFMigrationsHistory` table:** Records that the migrations have been applied.

## After Running the Command

-   A database file (e.g., `your_application.db`) should now exist in the `YourAppName.Web/bin/Debug/net6.0/` (or similar build output) directory.
-   This database will contain all the necessary tables for the application to run.

You should now be able to proceed with running the application.
