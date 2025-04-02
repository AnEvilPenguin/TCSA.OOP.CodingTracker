using Spectre.Console;
using TCSA.OOP.CodingTracker.Controllers;
using TCSA.OOP.CodingTracker.View;

var connection = DatabaseController.GetConnection();

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

// Take command line if possible
    // eg gh repo create
    // if args.length > 0 parse them and run otherwise prompty prompt

// Must contain a README file
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