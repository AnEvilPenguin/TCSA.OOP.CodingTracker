using Spectre.Console;
using Spectre.Console.Cli;
using TCSA.OOP.CodingTracker.Util;

namespace TCSA.OOP.CodingTracker.Commands;

internal class NewSessionSettings : CommandSettings
{
    // We can't do both decorators.
    // We can however supply two separate fields and then evaluate each in Validate
    [CommandArgument(0, "<SESSION_NAME>")]
    //[CommandOption("-n|--name <SESSION_NAME>")]
    public string SessionName { get; set; } = String.Empty;
    
    // [CommandArgument(1, "[START_TIME]")]
    // When providing the option it cannot be optional
    // Think about is as when passing -s there must also be a string afterwards
    [CommandOption("-s|--start <START_TIME>")]
    public string? StartTime { get; set; } = String.Empty;
    
    [CommandOption("-f|--finish <FINISH_TIME>")]
    public string? FinishTime { get; set; } = String.Empty;
    
    internal DateTime? Start { get; set; }
    internal DateTime? Finish { get; set; }

    public override ValidationResult Validate()
    {
        if (string.IsNullOrWhiteSpace(SessionName))
        {
            return ValidationResult.Error("SESSION_NAME is required.");
        }

        if (string.IsNullOrWhiteSpace(StartTime) && !string.IsNullOrWhiteSpace(FinishTime))
        {
            return ValidationResult.Error("START_TIME is required when FINISH_TIME is specified.");
        }

        DateTime startTime = default;
        
        if (!string.IsNullOrWhiteSpace(StartTime))
        {
            if(!DateTimeValidation.IsValidDateString(StartTime, out string validationError))
                return ValidationResult.Error($"START_TIME format error: {validationError}");
            
            DateTime.TryParse(StartTime, out startTime);
            
            if (!DateTimeValidation.IsValidDate(startTime, new DateTime(1954, 1, 1), DateTime.Now, out validationError))
                return ValidationResult.Error($"START_TIME validation error: {validationError}");
            
            Start = startTime;
        }
        
        if (!string.IsNullOrWhiteSpace(FinishTime))
        {
            if(!DateTimeValidation.IsValidDateString(FinishTime, out string validationError))
                return ValidationResult.Error($"FINISH_TIME format error: {validationError}");
            
            DateTime.TryParse(FinishTime, out DateTime finishTime);
            
            if (!DateTimeValidation.IsValidDate(finishTime, startTime, DateTime.Now, out validationError))
                return ValidationResult.Error($"FINISH_TIME validation error: {validationError}");
            
            Finish = finishTime;
        }
        
        return ValidationResult.Success();
    }
}