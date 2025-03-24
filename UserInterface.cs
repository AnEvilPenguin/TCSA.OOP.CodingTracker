using Spectre.Console;
using TCSA.OOP.CodingTracker.Controllers;

namespace TCSA.OOP.CodingTracker;

internal enum MenuOptions
{
    Exit
}

internal class UserInterface(SessionController sessionController)
{
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
            Console.Clear();
            
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOptions>()
                    .Title("What do you want to do next?")
                    .AddChoices(Enum.GetValues<MenuOptions>()));
            
            if (choice == MenuOptions.Exit)
                return 0;
            
            AnsiConsole.MarkupLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}