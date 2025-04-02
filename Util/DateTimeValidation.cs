using System.Text.RegularExpressions;
using static TCSA.OOP.CodingTracker.Util.Helpers;

namespace TCSA.OOP.CodingTracker.Util;

internal static class DateTimeValidation
{
    private static readonly Regex DateTimeStringMatcher = new ("^\\d{4}(-\\d{2}){2} \\d{2}:\\d{2}");

    internal static bool IsValidDate(DateTime date, DateTime minDate, DateTime maxDate, out string validationError)
    {
        if (maxDate < date)
        {
            validationError = $"{FormatDate(date)} is newer than {FormatDate(maxDate)}.";
            return false;
        }

        if (minDate > date)
        {
            validationError = $"{FormatDate(date)} is older than {FormatDate(minDate)}.";
            return false;
        }
        
        validationError = string.Empty;
        return true;
    }

    internal static bool IsValidDateString(string dateString, out string validationError)
    {
        if (DateTimeStringMatcher.IsMatch(dateString))
        {
            validationError = string.Empty;
            return true;
        }

        validationError = "Date string is not in the expected format: yyyy-MM-dd HH:mm";
        return false;
    }
}