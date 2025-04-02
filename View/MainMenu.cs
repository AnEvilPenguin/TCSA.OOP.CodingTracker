using Spectre.Console;
using Spectre.Console.Cli;
using TCSA.OOP.CodingTracker.Commands;
using TCSA.OOP.CodingTracker.Controllers;
using TCSA.OOP.CodingTracker.Model;
using static TCSA.OOP.CodingTracker.View.SessionView;

namespace TCSA.OOP.CodingTracker.View;

internal enum MenuOptions
{
    Sessions,
    Reports,
    Exit
}

internal class MainMenu(SessionController sessionController) : AbstractMenu
{
    private const string ExampleDate = "(e.g. 2020-02-27 14:30)";
    
    private readonly SessionMenu _sessionMenu = new (sessionController);
    private readonly ReportMenu _reportMenu = new (sessionController);
    
    internal int Run(string[] args)
    {
        if (args.Length == 0)
            return RunMenu();

        return RunCommandLine(args);
    }

    private int RunMenu()
    {
        while (true)
        {
            AnsiConsole.Clear();

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOptions>()
                    .Title("What do you want to do next?")
                    .AddChoices(Enum.GetValues<MenuOptions>()));

            switch (choice)
            {
                case MenuOptions.Sessions:
                    _sessionMenu.RunMenu();
                    break;
                
                case MenuOptions.Reports:
                    _reportMenu.RunMenu();
                    break;
                
                case MenuOptions.Exit:
                    return 0;
                
                default:
                    throw new NotImplementedException($"{choice} is not implemented.");
            }
        }
    }

    private int RunCommandLine(string[] args)
    {
        var app = new CommandApp();
        app.Configure(config =>
        {
            config.AddBranch("Utils", (settings) =>
            {
                settings.AddCommand<GenerateSampleData>("GenerateSampleData")
                    .WithDescription("Generate sample data to test the application.")
                    .WithData(sessionController);
                
                settings.AddCommand<ClearSessions>("ClearSessions")
                    .WithDescription("Clear all data from the application.")
                    .WithData(sessionController);
            });
            
            config.AddBranch("Session", (settings) =>
            {
                settings.AddCommand<SessionCommands.NewSession>("New")
                    .WithDescription("Create a new session.")
                    .WithData(sessionController);
                
                settings.AddCommand<SessionCommands.GetSession>("Get")
                    .WithDescription("Get a session by id.")
                    .WithData(sessionController);
                
                settings.AddCommand<SessionCommands.UpdateSession>("Update")
                    .WithDescription("Update a session.")
                    .WithData(sessionController);
                
                settings.AddCommand<SessionCommands.ListSessions>("List")
                    .WithDescription("List all sessions.")
                    .WithData(sessionController);
                
                settings.AddCommand<SessionCommands.ListOpenSessions>("ListOpen")
                    .WithDescription("List open sessions.")
                    .WithData(sessionController);
            });
        });
        
        return app.Run(args);
    }
}