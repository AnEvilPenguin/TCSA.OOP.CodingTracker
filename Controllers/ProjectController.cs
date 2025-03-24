using Spectre.Console;
using TCSA.OOP.CodingTracker.Model;
using TCSA.OOP.CodingTracker.View;

namespace TCSA.OOP.CodingTracker.Controllers;

internal class ProjectController
{
    private readonly CrudController _controller = new();

    internal int List()
    {
        try
        {
            IEnumerable<Project> projects = _controller.ListProjects();
            TableView.List(projects);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error:[/] {ex.Message}");
            return 1;
        }

        return 0;
    }
}