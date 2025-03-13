namespace TCSA.OOP.CodingTracker.Model;

internal class Project
{
    internal int Id { get; set; }
    internal required string Name { get; set; }
    internal DateTime Created { get; set; }
    internal DateTime Updated { get; set; }
    internal string? Repository { get; set; }
}