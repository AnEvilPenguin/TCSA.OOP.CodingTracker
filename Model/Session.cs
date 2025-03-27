namespace TCSA.OOP.CodingTracker.Model;

internal class Session
{
    internal int Id { get; init; }
    internal required string Name { get; init; }
    internal DateTime Created { get; init; } = DateTime.UtcNow;
    internal DateTime Updated { get; set; } = DateTime.UtcNow;
    internal DateTime Started { get; init; } = DateTime.UtcNow;
    internal DateTime? Finished { get; set; }
    internal TimeSpan Duration => (Finished ?? DateTime.UtcNow) - Started;

    internal string GetDuration()
    {
        var duration = (Finished ?? DateTime.UtcNow) - Started;
        
        if (Finished.HasValue)
            return duration.ToString(@"d\ \-\ hh\:mm\:ss");
        
        return  $@"{duration:d\ \-\ hh\:mm\:ss} (Ongoing)";
    }
}