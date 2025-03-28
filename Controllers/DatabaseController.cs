using System.Configuration;
using System.Data.SQLite;

namespace TCSA.OOP.CodingTracker.Controllers;

internal static class DatabaseController
{
    internal static SQLiteConnection GetConnection()
    {
        var connectionString = ConfigurationManager.ConnectionStrings["Tracker"].ConnectionString;
        var connection = new SQLiteConnection(connectionString);
        
        return connection;
    }
}