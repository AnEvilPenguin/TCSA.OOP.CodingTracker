using TCSA.OOP.CodingTracker.Controllers;
using System.Configuration;

var connectionController = new DataController();

var connection = connectionController.Initialize();

// Requirements
// Log time spent coding
    // Perhaps some shortcuts to say open a 'session' that can be closed off on completion later
    // Cannot enter the duration, can only enter start and end times
    // Specifically calls out a 'CalculateDuration' method
// Need to use Spectre.Console for input and output
// Separation of concerns
// Prompt user with a specific format for date and time to be in and not allow any other format
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