using Spectre.Console;
using TCSA.OOP.CodingTracker.Controllers;
using TCSA.OOP.CodingTracker.Model;

namespace TCSA.OOP.CodingTracker.View;

internal static class TableView
{
    internal static void List(IEnumerable<Project> projects)
    {
        var table = new Table();
        
        table.AddColumn(new TableColumn("Id").Centered());
        table.AddColumn(new TableColumn("Name").Centered());
        table.AddColumn(new TableColumn("Repository").Centered());
        
        foreach (var project in projects)
        {
            table.AddRow(project.Id.ToString(), project.Name, project.Repository ?? "");
        }
        
        AnsiConsole.Write(table);
    }
}