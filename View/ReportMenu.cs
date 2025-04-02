using Spectre.Console;
using TCSA.OOP.CodingTracker.Controllers;
using static TCSA.OOP.CodingTracker.View.ReportView;

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
            
            Pause();
        }
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
        
        var final = aggregated
            .Select(t => 
                new Tuple<string, int>(t.Item1.ToString("yyyy MMMM"), t.Item2));
        
        DisplayBarChart("Sessions per month", final, (int value) =>
        {
            return value switch
            {
                < 5 => Color.Red,
                < 15 => Color.Yellow,
                _ => Color.Green
            };
        });
    }
}