namespace TCSA.OOP.CodingTracker.Model;

internal class Session
{
    internal int Id { get; set; }
    internal required string Name { get; set; }
    internal DateTime Created { get; set; }
    internal DateTime Updated { get; set; }
    internal DateTime Started { get; set; }
    internal DateTime Finished { get; set; }
    internal required Project Project { get; set; }
}