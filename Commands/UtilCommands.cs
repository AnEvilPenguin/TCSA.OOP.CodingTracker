using Bogus;
using Spectre.Console;
using Spectre.Console.Cli;
using TCSA.OOP.CodingTracker.Controllers;
using TCSA.OOP.CodingTracker.Model;

namespace TCSA.OOP.CodingTracker.Commands;

internal class GenerateSampleData : Command<UtilSettings>
{
    public override int Execute(CommandContext context, UtilSettings settings)
    {
        if (context.Data is not SessionController sessionController)
            throw new ApplicationException("Session controller is not set.");
        
        var sessionFaker = new Faker<Session>()
            .RuleFor(s => s.Name, f => $"{f.Hacker.Noun()}-{f.Commerce.Product()}")
            .RuleFor(s => s.Started, f => f.Date.Past())
            .RuleFor(
                s=> s.Finished, 
                (f,s) => f.Date.Between(s.Started, s.Started.AddDays(3)));
        
        var sessions = sessionFaker.Generate(20);
        
        var openSessionFaker = new Faker<Session>()
            .RuleFor(s => s.Name, f => $"{f.Hacker.Noun()}-{f.Hacker.Noun()}")
            .RuleFor(s => s.Started, f => f.Date.Recent());
        
        var openSessions = openSessionFaker.Generate(1);

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