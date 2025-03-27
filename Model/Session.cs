namespace TCSA.OOP.CodingTracker.Model;

internal class Session
{
    internal int Id { get; init; }
    internal required string Name { get; init; }
    internal DateTime Created { get; init; } = DateTime.UtcNow;
    internal DateTime Updated { get; set; } = DateTime.UtcNow;
    internal DateTime Started { get; init; } = DateTime.UtcNow;
    internal DateTime? Finished { get; set; }
}