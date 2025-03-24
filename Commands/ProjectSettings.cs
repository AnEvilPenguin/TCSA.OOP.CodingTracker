using Spectre.Console.Cli;
using TCSA.OOP.CodingTracker.Controllers;
using TCSA.OOP.CodingTracker.Model;

namespace TCSA.OOP.CodingTracker.View;

internal class ProjectSettings : CommandSettings
{
    internal ProjectController Controller { get; private set; } = new(); 
}