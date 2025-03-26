using Spectre.Console;
using TCSA.OOP.CodingTracker.Controllers;
using TCSA.OOP.CodingTracker.Model;
using static TCSA.OOP.CodingTracker.View.SessionView;

namespace TCSA.OOP.CodingTracker.View;

internal enum SessionOptions
{
    New,
    End,
    List,
    ListOpen,
    Back
}
internal class SessionMenu (SessionController sessionController) : AbstractMenu
{
    private const string ExampleDate = "(e.g. 2020-02-27 14:30)";
    
    internal void RunMenu()
    {
        while (true)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<SessionOptions>()
                    .Title("What do you want to do next?")
                    .AddChoices(Enum.GetValues<SessionOptions>()));

            switch (choice)
            {
                case SessionOptions.Back:
                    return;
                
                case SessionOptions.New:
                    NewSession();
                    break;
                
                case SessionOptions.List:
                    AllSessions();
                    break;
                
                case SessionOptions.ListOpen:
                    OpenSessions();
                    break;
                
                case SessionOptions.End:
                    EndSession();
                    break;
                
                default:
                    throw new NotImplementedException($"{choice} is not implemented.");
            }
            
            Pause();
        }
    }

    private void NewSession()
    {
        var count = sessionController.ListOpen().Count();

        if (count > 0)
        {
            AnsiConsole.MarkupLine($"[yellow]Warn:[/] There are already [orange]{count}[/] session(s) open.");
            if (!AnsiConsole.Confirm("Do you want to open another session?"))
                return;
        }
        
        var sessionName = AnsiConsole.Ask<string>("What do you want to [green]name[/] the session?");
        
        var start = GetStartDate();
        var end = GetFinishDate(start);

        var session = sessionController.Create(sessionName, start, end);
        
        DisplaySession(session);
    }
    
    private DateTime? GetStartDate()
    {
        if (AnsiConsole.Confirm("Do you want to start the session now?"))
        {
            return null;
        }
        
        return GetDate("Provide a date time for the session start");
    }

    private DateTime? GetFinishDate(Session session) =>
        PromptFinishDate(session.Started);

    private DateTime? GetFinishDate(DateTime? start)
    {
        if (start == null)
            return null;
        
        if (!AnsiConsole.Confirm("Do you want to finish the session?"))
            return null;
        
        return PromptFinishDate(start);
    }

    private DateTime PromptFinishDate(DateTime? start) =>
        AnsiConsole.Confirm("Do you want to finish the session now?")
            ? DateTime.UtcNow
            // TODO pass start to getdate as lower bound
            : GetDate("Provide a date time for the session end");

    private DateTime GetDate(string prompt)
    {
        // TODO bounds and validation
        return AnsiConsole.Ask<DateTime>($"{prompt}{ExampleDate}");
    }

    private void AllSessions() =>
        DisplaySessions(sessionController.List());

    private void OpenSessions() =>
        DisplaySessions(sessionController.ListOpen());

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
        
        session.Finished = GetFinishDate(session);
        
        sessionController.Update(session);
        
        DisplaySession(session);
    }
}