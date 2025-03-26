using System.Data.SQLite;
using Dapper;
using TCSA.OOP.CodingTracker.Model;

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

    
    internal Session Create(string name)
    {
        const string sql = @"
            INSERT INTO Sessions (Name, Created, Updated, Started) 
                VALUES (@Name, @Created, @Updated, @Started) 
                RETURNING id;
        ";
        var now = DateTime.UtcNow;
        var id = _connection!.QuerySingle<int>(sql, new
        {
            Name = name, Created = now, Updated = now, Started = now
        });

        return new Session { Id = id, Name = name, Created = now, Updated = now, Started = now };
    }

    internal Session Create(string name, DateTime? startDate, DateTime? finishDate)
    {
        const string sql = @"
            INSERT INTO Sessions (Name, Created, Updated, Started, Finished) 
                VALUES (@Name, @Created, @Updated, @Started, @Finished) 
                RETURNING id;
        ";
        var now = DateTime.UtcNow;
        var id = _connection!.QuerySingle<int>(sql, new
        {
            Name = name, Created = now, Updated = now, Started = startDate ?? now, Finished = finishDate
        });
        
        return new Session
        {
            Id = id, Name = name, Created = now, Updated = now, Started = startDate ?? now, Finished = finishDate
        };
    }

    internal Session Get(int id)
    {
        const string sql = @"
            SELECT * FROM Sessions
            WHERE Id = @Id
        ";

        return _connection!.QuerySingle<Session>(sql, new { Id = id });
    }

    internal IEnumerable<Session> List()
    {
        const string sql = @"
            SELECT * FROM Sessions
        ";

        return _connection!.Query<Session>(sql);
    }

    internal IEnumerable<Session> ListOpen()
    {
        const string sql = @"
            SELECT * FROM Sessions
            WHERE Finished IS NULL
        ";

        return _connection!.Query<Session>(sql);
    }

    internal void Update(Session session)
    {
        session.Updated = DateTime.UtcNow;

        const string sql = @"
            UPDATE Sessions 
            SET Name = @Name, Updated = @Updated, Started = @Started, Finished = @Finished
            WHERE Id = @Id
        ";

        _connection!.Execute(sql, new { session.Name, session.Updated, session.Started, session.Finished, session.Id });
    }

    internal void Delete(Session session)
    {
        const string sql = @"
            DELETE FROM Sessions
            WHERE Id = @Id
        ";

        _connection!.Execute(sql, new { session.Id });
    }
}