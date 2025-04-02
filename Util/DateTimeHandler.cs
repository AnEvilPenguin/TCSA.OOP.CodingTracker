using System.Data;
using Dapper;

namespace TCSA.OOP.CodingTracker.Util;

public class DateTimeHandler : SqlMapper.TypeHandler<DateTime>
{
    public override void SetValue(IDbDataParameter parameter, DateTime value)
    {
        parameter.Value = FormatDate(value);
    }

    public override DateTime Parse(object value)
    {
        if (value.ToString()!.EndsWith("Z"))
        {
            // If the value is explicitly marked as UTC with 'Z', parse as UTC
            return DateTime.Parse(value.ToString()!, null, System.Globalization.DateTimeStyles.AdjustToUniversal);
        }

        // Otherwise, interpret as local time or leave as unspecified
        return DateTime.Parse(value.ToString()!);
    }
    
    private string FormatDate(DateTime date) =>
        date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
}