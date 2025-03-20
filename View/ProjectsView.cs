using Spectre.Console;
using TCSA.OOP.CodingTracker.Controllers;
using TCSA.OOP.CodingTracker.Model;

namespace TCSA.OOP.CodingTracker.View;

internal class ProjectsView
{
    private readonly CrudController _controller = new();

    internal int List()
    {
        IEnumerable<Project> projects;
        try
        {
            projects = _controller.ListProjects();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error:[/] {ex.Message}");
            return 1;
        }
        
        var table = new Table();
        
        table.AddColumn(new TableColumn("Id").Centered());
        table.AddColumn(new TableColumn("Name").Centered());
        table.AddColumn(new TableColumn("Repository").Centered());
        
        foreach (var project in projects)
        {
            table.AddRow(project.Id.ToString(), project.Name, project.Repository ?? "");
        }
        
        AnsiConsole.Write(table);
        
        return 0;
    }
}