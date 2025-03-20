using Spectre.Console.Cli;

namespace TCSA.OOP.CodingTracker.View;

internal class ProjectListCommand : Command<ProjectSettings>
{
    public override int Execute(CommandContext context, ProjectSettings settings) =>
        settings.View.List();
}