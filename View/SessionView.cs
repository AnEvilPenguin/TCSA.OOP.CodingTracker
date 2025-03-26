using Spectre.Console;
using TCSA.OOP.CodingTracker.Model;

namespace TCSA.OOP.CodingTracker.View;

internal static class SessionView
{
    internal static void DisplaySession(Session session)
    {
        AnsiConsole.Clear();
        
        var table = new Table();

        table.AddColumn(new TableColumn("Property"));
        table.AddColumn(new TableColumn("Value"));

        table.AddRow("Id", $"{session.Id}");
        table.AddRow("Name", session.Name);
        table.AddRow("Started", session.Started.ToShortDateString());
        table.AddRow("Finished", session.Finished?.ToShortDateString() ?? string.Empty);
        table.AddRow("Created", session.Created.ToShortDateString());
        table.AddRow("Updated", session.Updated.ToShortDateString());

        AnsiConsole.Write(table);
    }

    internal static void DisplaySessions(IEnumerable<Session> sessions)
    {
        AnsiConsole.Clear();
        
        var table = new Table();

        table.AddColumn(new TableColumn("Id").Centered());
        table.AddColumn(new TableColumn("Name").Centered());
        table.AddColumn(new TableColumn("Started").Centered());
        table.AddColumn(new TableColumn("Finished").Centered());

        foreach (var session in sessions)
            table.AddRow(
                $"{session.Id}",
                session.Name,
                session.Started.ToShortDateString(),
                session.Finished?.ToShortDateString() ?? string.Empty
            );

        AnsiConsole.Write(table);
    }
}