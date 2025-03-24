using System.Configuration;
using System.Data.SQLite;

namespace TCSA.OOP.CodingTracker.Controllers;

internal class SessionController
{
    private readonly SQLiteConnection _connection;

    private SessionController(SQLiteConnection connection)
    {
        _connection = connection;
    }

    internal static SessionController GetSessionController(SQLiteConnection connection)
    {
        var controller = new SessionController(connection);
        
        controller.Initialize();
        
        return controller;
    }

    private void Initialize()
    {
        _connection.Open();
        
        InitSessionsTable();
        
        _connection.Close();
    }

    private void InitSessionsTable()
    {
        var query = @"
            CREATE TABLE IF NOT EXISTS Sessions (
                'Id' INTEGER PRIMARY KEY AUTOINCREMENT,
                'Name' TEXT NOT NULL,
                'Created' TEXT NOT NULL,
                'Updated' TEXT NOT NULL,
                'Started' TEXT NOT NULL,
                'Finished' TEXT
            );";
        
        new SQLiteCommand(query, _connection).ExecuteNonQuery();
    }
    
    // TODO CRUD
}