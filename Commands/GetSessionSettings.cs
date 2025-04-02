using Spectre.Console;
using Spectre.Console.Cli;

namespace TCSA.OOP.CodingTracker.Commands;

public class GetSessionSettings : CommandSettings
{
    [CommandArgument(0, "<SESSION_ID>")] 
    public int SessionId { get; set; } = -1;

    public override ValidationResult Validate()
    {
        if (SessionId < 0)
            return ValidationResult.Error("SESSION_ID is required.");
        
        return ValidationResult.Success();
    }
}