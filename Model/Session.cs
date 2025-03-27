namespace TCSA.OOP.CodingTracker.Model;

internal class Session
{
    internal int Id { get; set; }
    internal required string Name { get; set; }
    internal DateTime Created { get; set; } = DateTime.UtcNow;
    internal DateTime Updated { get; set; } = DateTime.UtcNow;
    internal DateTime Started { get; set; } = DateTime.UtcNow;
    internal DateTime? Finished { get; set; }
}