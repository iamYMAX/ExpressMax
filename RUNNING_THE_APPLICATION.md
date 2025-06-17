# Running The Application

This document provides instructions on how to run the `YourAppName.Web` application.

## Prerequisites

1.  **.NET SDK:** Ensure you have the .NET SDK installed (version 6.0 or later).
2.  **Database Setup:** You MUST set up the database by applying migrations first. Please follow the instructions in `DATABASE_SETUP.md`. Without this, the application will not run correctly and login will fail.
3.  **Solution File:** This project uses a solution file (`YourAppName.sln`). You can open this file in an IDE like Visual Studio or JetBrains Rider.

## Running from the Command Line (CLI)

1.  **Open your terminal or command prompt.**
2.  **Navigate to the `YourAppName.Web` project directory:**
    ```bash
    cd path/to/your/solution/YourAppName.Web
    ```
    (Replace `path/to/your/solution/` with the actual path to where you cloned/placed the solution).
3.  **Run the application using the `dotnet run` command:**
    ```bash
    dotnet run
    ```
4.  **Access the application:**
    - The command output will tell you which URLs the application is listening on (e.g., `http://localhost:5000` and `https://localhost:5001`).
    - If your browser doesn't open automatically, open it manually and navigate to one of these URLs, specifically the HTTPS one if available (e.g., `https://localhost:5001/Account/Login` as configured in `launchSettings.json`).

## Running from an IDE (e.g., Visual Studio, JetBrains Rider)

1.  **Open the Solution:** Open the `YourAppName.sln` file in your IDE.
2.  **Set Startup Project:** Ensure `YourAppName.Web` is set as the startup project.
    - In Visual Studio: Right-click on the `YourAppName.Web` project in Solution Explorer -> "Set as Startup Project".
    - In Rider: The run configurations should automatically pick up profiles from `launchSettings.json`. You can select `YourAppName.Web` (Kestrel) or `IIS Express` profile from the run configurations dropdown.
3.  **Run/Debug:** Click the "Run" or "Debug" button (often a green play icon).
    - This will build the solution and start the `YourAppName.Web` application.
    - Your default web browser should open automatically to the application's launch URL (configured as `Account/Login` in `launchSettings.json`).

## First Login

- After setting up the database (see `DATABASE_SETUP.md`), an administrator account is automatically created:
  - **Username:** `admin@example.com`
  - **Password:** `Password123!`
- Use these credentials to log in for the first time.

## Troubleshooting
- **Port Conflicts:** If you get errors about ports being in use, you can change the ports in `YourAppName.Web/Properties/launchSettings.json`.
- **Database Errors:** Double-check that you have run the database migrations as described in `DATABASE_SETUP.md`.
