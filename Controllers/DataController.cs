using System.Configuration;
using System.Data.SQLite;

namespace TCSA.OOP.CodingTracker.Controllers;

internal class DataController ()
{
    internal SQLiteConnection Initialize()
    {
        var defaultProject = Boolean.Parse(ConfigurationManager.AppSettings.Get("CreateDefaultProject") ?? "false");
        var seedTestData = Boolean.Parse(ConfigurationManager.AppSettings.Get("SeedTestData") ?? "false");
        
        var connectionString = ConfigurationManager.ConnectionStrings["Tracker"].ConnectionString;
        
        var connection = new SQLiteConnection(connectionString);
     
        connection.Open();
        
        InitProjectsTable(connection);
        InitSessionsTable(connection);
        
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
                'Finished' TEXT NOT NULL,
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
}