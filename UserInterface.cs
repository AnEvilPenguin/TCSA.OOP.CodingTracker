using Spectre.Console;
using TCSA.OOP.CodingTracker.Controllers;
using static TCSA.OOP.CodingTracker.View.SessionView;

namespace TCSA.OOP.CodingTracker;

internal enum MenuOptions
{
    StartSession,
    ListSessions,
    ListOpenSessions,
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

            switch (choice)
            {
                case MenuOptions.StartSession:
                    StartSession();
                    break;

                case MenuOptions.ListSessions:
                    ListSessions();
                    break;
                
                case MenuOptions.ListOpenSessions:
                    ListOpenSessions();
                    break;
                    

                default:
                    throw new NotImplementedException($"{choice} is not implemented.");
            }

            AnsiConsole.MarkupLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    private void StartSession()
    {
        // TODO check if a session is already running
        // If already running check if we want to close that off
        // If not already running ask for start time
        // Ask if want to set end time.
        // If want to set end time do it.

        var sessionName = AnsiConsole.Ask<string>("What do you want to [green]name[/] the session?");
        // TODO more options in general

        var session = sessionController.Create(sessionName);

        DisplaySession(session);
    }

    private void ListSessions()
    {
        var sessions = sessionController.List();
        DisplaySessions(sessions);
    }

    private void ListOpenSessions()
    {
        var sessions = sessionController.ListOpen();
        DisplaySessions(sessions);
    }

    // End session
    // if no session in play return to menu
    // otherwise take end date and go nuts
}