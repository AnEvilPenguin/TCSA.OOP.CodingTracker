using Spectre.Console;

namespace TCSA.OOP.CodingTracker.View;

internal static class ReportView
{
    internal static void DisplayBarChart(string title, IEnumerable<Tuple<string, int>> data, Func<int, Color> getColor)
    {
        AnsiConsole.Clear();
        
        var chart = new BarChart()
            .Width(80)
            .Label($"[green bold underline]{title}[/]")
            .CenterLabel();

        foreach (var d in data)
        {
            var color = getColor(d.Item2);
            
            chart.AddItem(d.Item1, d.Item2, color);
        }
        
        AnsiConsole.Write(chart);
    }
}