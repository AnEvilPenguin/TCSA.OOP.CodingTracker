using Spectre.Console;
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

        throw new NotImplementedException("Command line parameters are not implemented.");
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
}