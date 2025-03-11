using System.Data.SQLite;

namespace TCSA.OOP.CodingTracker.Controllers;

internal class ConnectionController (string connectionString)
{
    internal SQLiteConnection Initialize()
    {
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