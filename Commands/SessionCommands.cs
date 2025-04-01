using Spectre.Console;
using Spectre.Console.Cli;
using TCSA.OOP.CodingTracker.Controllers;
using static TCSA.OOP.CodingTracker.View.SessionView;

namespace TCSA.OOP.CodingTracker.Commands;

internal class NewSession : Command<NewSessionSettings>
{
    public override int Execute(CommandContext context, NewSessionSettings settings)
    {
        if (context.Data is not SessionController sessionController)
            throw new ApplicationException("Session controller is not set.");

        try
        {
            sessionController.Create(settings.SessionName, settings.Start ?? DateTime.Now, settings.Finish);
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
            return 3;
        }
        
        return 0;
    }
}

internal class ListSessions : Command<SessionSettings>
{
    public override int Execute(CommandContext context, SessionSettings settings)
    {
        if (context.Data is not SessionController sessionController)
            throw new ApplicationException("Session controller is not set.");
        
        var sessions = sessionController.List();
        
        DisplaySessions(sessions);
        
        return 0;
    }
}