using Spectre.Console;
using TCSA.OOP.CodingTracker.Controllers;
using TCSA.OOP.CodingTracker.Model;
using static TCSA.OOP.CodingTracker.View.SessionView;

namespace TCSA.OOP.CodingTracker;

internal enum MenuOptions
{
    StartSession,
    EndSession,
    ListSessions,
    ListOpenSessions,
    Exit
}

internal class UserInterface(SessionController sessionController)
{
    private const string ExampleDate = "(e.g. 2020-02-27 14:30)";
    
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

            if (choice == MenuOptions.Exit)
                return 0;

            switch (choice)
            {
                case MenuOptions.StartSession:
                    StartSession();
                    break;
                
                case MenuOptions.EndSession:
                    EndSession();
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
        
        var session = sessionController.Create(sessionName);
        
        bool hasChanges = false;

        var startsNow = AnsiConsole.Confirm("Do you want to start the session now?");
        
        if (!startsNow)
        {
            DateTime newDate;
            
            do
            {
                newDate = AnsiConsole.Ask<DateTime>($"Provide a date time for the session start {ExampleDate}");
            } while(!session.IsValidDate(newDate));
            
            session.Started = newDate;
            hasChanges = true;
        }

        // TODO consider bumping this off to another method?
        if (!startsNow && AnsiConsole.Confirm("Do you want to finish the session?"))
        {
            DateTime endDate;
            do
            {
                endDate = AnsiConsole.Confirm("Do you want to finish the session now?")
                    ? DateTime.UtcNow
                    : AnsiConsole.Ask<DateTime>($"Provide a date time for the session end {ExampleDate}");
            }while(!session.IsValidFinishDate(endDate));

            session.Finished = endDate;
            hasChanges = true;
        }
        
        if (hasChanges)
            sessionController.Update(session);

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

    private void EndSession()
    {
        var sessions = sessionController.ListOpen().ToList();

        if (sessions.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No sessions available to end.[/]");
            return;
        }
        
        var session = AnsiConsole.Prompt(
            new SelectionPrompt<Session>()
                .Title("Select a [green]session[/] to complete:")
                .UseConverter(b => b.Name)
                .AddChoices(sessions));

        session.Finished = DateTime.UtcNow;
        
        sessionController.Update(session);
        
        AnsiConsole.Clear();
        DisplaySession(session);
    }

    // End session
    // if no session in play return to menu
    // otherwise take end date and go nuts
}