﻿using System.Configuration;
using System.Data.SQLite;
using Spectre.Console;
using TCSA.OOP.CodingTracker;
using TCSA.OOP.CodingTracker.Controllers;
using TCSA.OOP.CodingTracker.View;

var connectionString = ConfigurationManager.ConnectionStrings["Tracker"].ConnectionString;
var connection = new SQLiteConnection(connectionString);

SessionController sessionController;

try
{
    sessionController = SessionController.GetSessionController(connection);
}
catch (Exception ex)
{
    AnsiConsole.MarkupLine($"[red]Error:[/] {ex.Message}");
    return 1;
}

return new MainMenu(sessionController).Run(args);


// Requirements
// Log time spent coding
// Log time spent coding
    // Perhaps some shortcuts to say open a 'session' that can be closed off on completion later
    // Cannot enter the duration, can only enter start and end times
    // Specifically calls out a 'CalculateDuration' method
// Need to use Spectre.Console for input and output
// Separation of concerns
// Prompt user with a specific format for date and time to be in and not allow any other format
// Take command line if possible
    // eg gh repo create
    // if args.length > 0 parse them and run otherwise prompty prompt
// Use a configuration file to contain database path and connection strings
    // Perhaps guide the user through setting these if it doesn't exist yet?
// Need to use Dapper ORM for data access
    // Dapper does not support creating tables so these will have to be done using ADO.Net
// Cannot use anonymous objects, have to use lists of sessions, etc.
// Create and initialize a database if one is not present
// CRUD operations
// Must contain a README file
// Use parameterized queries
// Generate test data
// Reporting

// Allow tracking via stopwatch
// Allow filtering their records per period
// Allow ordering (in addition to filtering)
// Reports for total and average coding session per period
// Allow user to set goals and how far they are from their goal
    // Daily average goal?
    // Total goal
        // How many hours a day to reach their goal
        // How many days based on their average