using Spectre.Console;

namespace TCSA.OOP.CodingTracker.View;

internal abstract class AbstractMenu
{
    protected void Pause()
    {
        AnsiConsole.MarkupLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    protected void PrintError(string message)
    {
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[red]Error:[/] {message}");
        AnsiConsole.WriteLine();
    }
    
    protected void PrintError(List<string> messages)
    {
        AnsiConsole.WriteLine();

        var first = messages.First();
        AnsiConsole.MarkupLine($"[red]Error:[/] {first}");
        
        foreach (var message in messages.Skip(1))
            AnsiConsole.MarkupLine(message);
        
        AnsiConsole.WriteLine();
    }
}