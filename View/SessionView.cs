using Spectre.Console;
using TCSA.OOP.CodingTracker.Model;
using static TCSA.OOP.CodingTracker.Util.Helpers;

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
        table.AddRow("Started", FormatDate(session.Started));
        table.AddRow("Finished", session.Finished.HasValue ? FormatDate(session.Finished.Value) : string.Empty);
        table.AddRow("Duration", session.GetDuration());

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
        table.AddColumn(new TableColumn("Duration").Centered());

        foreach (var session in sessions)
            table.AddRow(
                $"{session.Id}",
                session.Name,
                session.Started.ToShortDateString(),
                session.Finished?.ToShortDateString() ?? string.Empty,
                session.GetDuration()
            );

        AnsiConsole.Write(table);
    }
}