using Spectre.Console;
using Spectre.Console.Cli;
using TCSA.OOP.CodingTracker.Controllers;
using TCSA.OOP.CodingTracker.Model;
using static TCSA.OOP.CodingTracker.View.SessionView;

namespace TCSA.OOP.CodingTracker.Commands;

internal static class SessionCommands
{
    private static SessionController GetSessionController(CommandContext context)
    {
        if (context.Data is not SessionController sessionController)
            throw new ApplicationException("Session controller is not set.");
        
        return sessionController;
    }
    
    internal class NewSession : Command<NewSessionSettings>
    {
        public override int Execute(CommandContext context, NewSessionSettings settings)
        {
            var sessionController = GetSessionController(context);
    
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

    internal class GetSession : Command<GetSessionSettings>
    {
        public override int Execute(CommandContext context, GetSessionSettings settings)
        {
            var sessionController = GetSessionController(context);

            Session session;
            try
            {
                session = sessionController.Get(settings.SessionId);
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex);
                return 4;
            }

            DisplaySession(session);
            
            return 0;
        }
    }
    
    internal class UpdateSession : Command<UpdateSessionSettings>
    {
        public override int Execute(CommandContext context, UpdateSessionSettings settings)
        {
            if (settings.Session == null)
            {
                AnsiConsole.MarkupLine("[bold red]Failed to look up session.");
                return 5;
            }
            
            var session = settings.Session;
            var sessionController = GetSessionController(context);
            
            session.Finished = settings.Finish;
            
            sessionController.Update(session);
            
            return 0;
        }
    }
    
    internal class ListSessions : Command<SessionSettings>
    {
        public override int Execute(CommandContext context, SessionSettings settings)
        {
            var sessionController = GetSessionController(context);
            
            var sessions = sessionController.List();
            
            DisplaySessions(sessions);
            
            return 0;
        }
    }
    
    internal class ListOpenSessions : Command<SessionSettings>
    {
        public override int Execute(CommandContext context, SessionSettings settings)
        {
            var sessionController = GetSessionController(context);
            
            var sessions = sessionController.ListOpen();
            
            DisplaySessions(sessions);
            
            return 0;
        }
    }
}

