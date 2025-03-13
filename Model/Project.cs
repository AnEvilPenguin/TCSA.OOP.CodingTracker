namespace TCSA.OOP.CodingTracker.Model;

internal class Project
{
    internal int Id { get; set; }
    internal required string Name { get; set; }
    internal DateTime Created { get; set; } = DateTime.UtcNow;
    internal DateTime Updated { get; set; } = DateTime.UtcNow;
    internal string? Repository { get; set; }
}