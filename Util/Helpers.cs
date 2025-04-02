namespace TCSA.OOP.CodingTracker.Util;

internal static class Helpers
{
    internal static string FormatDate(DateTime date) =>
        date.ToLocalTime().ToString("yyyy-MM-dd HH:mm");
}