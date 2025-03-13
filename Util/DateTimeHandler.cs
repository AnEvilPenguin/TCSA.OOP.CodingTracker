using System.Data;
using Dapper;
using static TCSA.OOP.CodingTracker.Util.Helpers;

namespace TCSA.OOP.CodingTracker.Util;

public class DateTimeHandler : SqlMapper.TypeHandler<DateTime>
{
    public override void SetValue(IDbDataParameter parameter, DateTime value)
    {
        parameter.Value = FormatDate(value);
    }

    public override DateTime Parse(object value)
    {
        return DateTime.SpecifyKind(DateTime.Parse(value.ToString()), DateTimeKind.Utc);
    }
}