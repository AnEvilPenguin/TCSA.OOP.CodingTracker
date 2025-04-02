using Spectre.Console;
using Spectre.Console.Cli;
using TCSA.OOP.CodingTracker.Controllers;
using TCSA.OOP.CodingTracker.Model;
using TCSA.OOP.CodingTracker.Util;

namespace TCSA.OOP.CodingTracker.Commands;

public class UpdateSessionSettings : GetSessionSettings
{
    [CommandOption("-f|--finish <FINISH_TIME>")]
    public string FinishTime { get; set; } = String.Empty;
    internal DateTime Finish { get; set; }
    internal Session? Session { get; set; }
    
    // Is there a better way of doing this?
    private SessionController _sessionController = SessionController
        .GetSessionController(DatabaseController.GetConnection());
    
    public override ValidationResult Validate()
    {
        var result = base.Validate();
        if (!result.Successful) 
            return result;

        if (string.IsNullOrWhiteSpace(FinishTime))
            return ValidationResult.Error("FINISH_TIME is required.");
        
        if(!DateTimeValidation.IsValidDateString(FinishTime, out string validationError))
            return ValidationResult.Error($"FINISH_TIME format error: {validationError}");
        
        DateTime.TryParse(FinishTime, out DateTime finishTime);
        
        try
        {
            Session = _sessionController.Get(SessionId);
        }
        catch (Exception ex)
        {
            return ValidationResult.Error("Failed to get session: " + ex.Message);
        }
        
        if (!DateTimeValidation.IsValidDate(finishTime, Session.Started, DateTime.Now, out validationError))
            return ValidationResult.Error($"FINISH_TIME validation error: {validationError}");
        
        Finish = finishTime;
        
        return ValidationResult.Success();
    }
}