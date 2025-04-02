using Spectre.Console;
using Spectre.Console.Cli;
using TCSA.OOP.CodingTracker.Controllers;

namespace TCSA.OOP.CodingTracker.Commands;

internal class GenerateSampleData : Command<UtilSettings>
{
    public override int Execute(CommandContext context, UtilSettings settings)
    {
        if (context.Data is not SessionController sessionController)
            throw new ApplicationException("Session controller is not set.");

        var sampleData = new SampleDataController();

        var sessions = sampleData.GenerateSessions(20);
        var openSessions = sampleData.GenerateOpenSessions(1);
        
        foreach (var session in sessions.Union(openSessions))
        {
            AnsiConsole.MarkupLine($"[green]Generating[/] [bold]{session.Name}[/]");
            sessionController.Create(session.Name, session.Started, session.Finished);
        }
        
        return 0;
    }
}

internal class ClearSessions : Command<UtilSettings>
{
    public override int Execute(CommandContext context, UtilSettings settings)
    {
        if (context.Data is not SessionController sessionController)
            throw new ApplicationException("Session controller is not set.");

        if (!AnsiConsole.Confirm(
                "Are you sure you want to clear the sessions? [red bold]This action [underline]cannot[/] be undone.[/]", false))
            return 0;

        foreach (var session in sessionController.List())
        {
            AnsiConsole.MarkupLine($"[red]Deleting[/] [bold]{session.Name}[/]");
            sessionController.Delete(session);
        }

        return 0;
    }
}