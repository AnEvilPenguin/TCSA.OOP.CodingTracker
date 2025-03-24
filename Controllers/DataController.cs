using System.Configuration;
using System.Data.SQLite;
using static TCSA.OOP.CodingTracker.Util.Helpers;

namespace TCSA.OOP.CodingTracker.Controllers;

internal class DataController ()
{
    // FIXME think about splitting this down better.
    // Have this just deal with a connection
    
    internal SQLiteConnection Initialize()
    {
        var defaultProject = Boolean.Parse(ConfigurationManager.AppSettings.Get("CreateDefaultProject") ?? "false");
        var seedTestData = Boolean.Parse(ConfigurationManager.AppSettings.Get("SeedTestData") ?? "false");
        
        var connectionString = ConfigurationManager.ConnectionStrings["Tracker"].ConnectionString;
        
        var connection = new SQLiteConnection(connectionString);
     
        connection.Open();
        
        InitProjectsTable(connection);
        InitSessionsTable(connection);
        
        if (defaultProject)
            InjectDefaultProject(connection);
        
        connection.Close();
        
        return connection;
    }

    private void InitSessionsTable(SQLiteConnection connection)
    {
        var query = @"
            CREATE TABLE IF NOT EXISTS Sessions (
                'Id' INTEGER PRIMARY KEY AUTOINCREMENT,
                'Name' TEXT NOT NULL,
                'Created' TEXT NOT NULL,
                'Updated' TEXT NOT NULL,
                'Started' TEXT NOT NULL,
                'Finished' TEXT,
                'Project' INTEGER NOT NULL,
                FOREIGN KEY('Project') REFERENCES 'Projects'('Id') ON UPDATE CASCADE
            );";
        
        new SQLiteCommand(query, connection).ExecuteNonQuery();
    }

    private void InitProjectsTable(SQLiteConnection connection)
    {
        var query = @"
            CREATE TABLE IF NOT EXISTS Projects (
                'Id' INTEGER PRIMARY KEY AUTOINCREMENT,
                'Name' TEXT NOT NULL,
                'Created' TEXT NOT NULL,
                'Updated' TEXT NOT NULL,
                'Repository' TEXT
            );";
        
        new SQLiteCommand(query, connection).ExecuteNonQuery();
    }

    private void InjectDefaultProject(SQLiteConnection connection)
    {
        var checkQuery = @"
            SELECT Id
            FROM Projects
            WHERE Name = 'Default Project'
            LIMIT 1;
        ";
        
        var exists = new SQLiteCommand(checkQuery, connection).ExecuteScalar();
        
        if (exists != null)
            return;
        
        var insertQuery = @"
            INSERT INTO Projects (Name, Created, Updated) VALUES($Name, $Created, $Updated);
        ";

        var command = new SQLiteCommand(insertQuery, connection);
        
        var date = DateTime.UtcNow;
        var dateString = FormatDate(date);
            
        command.Parameters.AddWithValue("$Name", "Default Project");
        command.Parameters.AddWithValue("$Created", dateString);
        command.Parameters.AddWithValue("$Updated", dateString);

        command.ExecuteScalar();
    }
}