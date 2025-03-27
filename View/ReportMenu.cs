using System.Reflection.Emit;
using Spectre.Console;
using TCSA.OOP.CodingTracker.Controllers;

namespace TCSA.OOP.CodingTracker.View;

internal enum ReportOptions
{
    MonthlyAverageSession,
    Back
}

internal class ReportMenu (SessionController sessionController) : AbstractMenu
{
    internal void RunMenu()
    {
        while (true)
        {
            var choice = GetSelection<ReportOptions>();

            switch (choice)
            {
                case ReportOptions.Back:
                    return;
                
                case ReportOptions.MonthlyAverageSession:
                    MonthlyAverageSession();
                    break;
                
                default:
                    throw new NotImplementedException($"{choice} is not implemented.");
            }
        }
        
        Pause();
    }

    private void MonthlyAverageSession()
    {
        var sessions = sessionController.List();

        var aggregated = sessions.Aggregate(new Dictionary<DateTime, int>(), (acc, cur) =>
        {
            var start = cur.Started;
            var limit = cur.Finished ?? DateTime.UtcNow;

            var iterator = new DateTime(start.Year, start.Month, 1);

            while (iterator <= limit)
            {
                acc.TryAdd(iterator, 0);
                
                acc[iterator]++;

                iterator = iterator.AddMonths(1);
            }
            
            return acc;
        }).Aggregate(new List<Tuple<DateTime, int>>(), (acc, cur) =>
        {
            acc.Add(new Tuple<DateTime, int>(cur.Key, cur.Value));
            return acc;
        });

        aggregated.Sort((a, b) => a.Item1.CompareTo(b.Item1));

        var chart = new BarChart()
            .Width(80)
            .Label("[green bold underline]Sessions per month[/]")
            .CenterLabel();

        // TODO Consider how to color things
        // could do a static value, or could potentially work out percentiles?
        foreach (var tuple in aggregated)
        {
            Color color;
            
            if (tuple.Item2 < 5)
                color = Color.Red;
            else if (tuple.Item2 < 15)
                color = Color.Yellow;
            else
                color = Color.Green;
            
            chart.AddItem(tuple.Item1.ToString("yyyy MMMM"), tuple.Item2, color);
        }
        
        AnsiConsole.Write(chart);
        Pause();
    }
}