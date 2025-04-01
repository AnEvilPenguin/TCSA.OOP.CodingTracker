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
        return DateTime.Parse(value.ToString()!);
    }
    
    private string FormatDate(DateTime date) =>
        date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
}