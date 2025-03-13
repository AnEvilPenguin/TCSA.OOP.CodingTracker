namespace TCSA.OOP.CodingTracker.Util;

internal static class Helpers
{
    internal static string FormatDate(DateTime date) =>
        date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
}