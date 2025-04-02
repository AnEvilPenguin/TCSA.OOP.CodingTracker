namespace TCSA.OOP.CodingTracker.Model;

internal class Session
{
    internal int Id { get; init; }
    internal required string Name { get; init; }
    internal DateTime Created { get; init; } = DateTime.UtcNow;
    internal DateTime Updated { get; set; } = DateTime.UtcNow;

    internal DateTime Started
    {
        get => _started;
        init
        {
            _started = value.ToUniversalTime();
        }
    }

    internal DateTime? Finished
    {
        get => _finished;
        set => _finished = value?.ToUniversalTime();
    }

    internal TimeSpan Duration => (Finished ?? DateTime.UtcNow) - Started;
    
    private DateTime? _finished;
    private DateTime _started = DateTime.UtcNow;

    internal string GetDuration()
    {
        var duration = (Finished ?? DateTime.UtcNow) - Started;
        
        if (Finished.HasValue)
            return duration.ToString(@"d\ \-\ hh\:mm\:ss");
        
        return  $@"{duration:d\ \-\ hh\:mm\:ss} (Ongoing)";
    }
}