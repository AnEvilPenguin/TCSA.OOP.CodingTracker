﻿namespace TCSA.OOP.CodingTracker.Model;

internal class Session
{
    // Do we actually want to have a bool for open sessions?
    // Might make things easier?
    internal int Id { get; set; }
    internal required string Name { get; set; }
    internal DateTime Created { get; set; } = DateTime.UtcNow;
    internal DateTime Updated { get; set; } = DateTime.UtcNow;
    internal DateTime Started { get; set; } = DateTime.UtcNow;
    internal DateTime Finished { get; set; }
    internal required Project Project { get; set; }
}