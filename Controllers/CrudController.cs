using System.Data.SQLite;
using Dapper;
using TCSA.OOP.CodingTracker.Model;
using TCSA.OOP.CodingTracker.Util;

namespace TCSA.OOP.CodingTracker.Controllers;

internal class CrudController()
{
    private readonly DataController _dataController = new DataController();
    private SQLiteConnection? _connection;

    // Sessions
    // Create
    // List
    // Get
    // Update
    // Delete

    internal IEnumerable<Session> ListSessions()
    {
        if (_connection == null)
            Initialize();

        const string sql = "SELECT * FROM Sessions JOIN Projects P on P.Id = Sessions.Project";
        return _connection!.Query<Session, Project, Session>(sql, (session, project) =>
            {
                session.Project = project;
                return session;
            },
            splitOn: "Project");
    }

    internal Session GetSession(int id)
    {
        if (_connection == null)
            Initialize();

        const string sql = @"
            SELECT * 
            FROM Sessions 
                JOIN Projects P on P.Id = Sessions.Project 
            WHERE Sessions.Id = @Id 
        ";

        return _connection!.QuerySingle<Session>(sql, new { Id = id });
    }

    internal void CreateSession(Session session)
    {
        if (_connection == null)
            Initialize();

        const string sql = @"
            INSERT INTO Sessions (Name, Created, Updated, Started, Project)
                VALUES (@Name, @Created, @Updated, @Started, @Project)
        ";

        _connection!.Execute(sql, new
        {
            session.Name, session.Created, session.Updated, session.Started, Project = session.Project.Id
        });
    }

    // Projects (Do I actually need all of these?)
    internal Project CreateProject(string name, string repository)
    {
        const string sql = @"
            INSERT INTO Projects (Name, Created, Updated, Repository) 
                VALUES (@Name, @Created, @Updated, @Repository) 
                RETURNING id;
        ";
        var now = DateTime.UtcNow;
        var id = _connection!.QuerySingle<int>(sql, new
        {
            Name = name, Created = now, Updated = now, Repository = repository
        });

        return new Project() { Id = id, Name = name, Created = now, Updated = now };
    }

    internal IEnumerable<Project> ListProjects()
    {
        if (_connection == null)
            Initialize();

        const string sql = "SELECT * FROM Projects";
        return _connection!.Query<Project>(sql);
    }

    internal IEnumerable<Project> ListProjects(string name)
    {
        if (_connection == null)
            Initialize();

        const string sql = "SELECT * FROM Projects WHERE Name = @Name";
        return _connection!.Query<Project>(sql, new { Name = name });
    }

    internal Project GetProject(int projectId)
    {
        if (_connection == null)
            Initialize();

        const string sql = "SELECT * FROM Projects WHERE Id = @ProjectId";
        return _connection!.QuerySingle<Project>(sql, new { ProjectId = projectId });
    }

    internal void UpdateProject(Project project)
    {
        if (_connection == null)
            Initialize();

        project.Updated = DateTime.UtcNow;

        const string sql = @"
            UPDATE Projects 
            SET Name = @Name, Updated = @Updated, Repository = @Repository
            WHERE Id = @Id
        ";

        _connection!.Execute(sql, new { project.Name, project.Updated, project.Repository, project.Id });
    }

    internal void DeleteProject(Project project)
    {
        if (_connection == null)
            Initialize();

        const string sql = "DELETE FROM Projects WHERE Id = @Id";
        _connection!.Execute(sql, new { project.Id });
    }

    private void Initialize()
    {
        if (_connection != null)
            return;

        _connection = _dataController.Initialize();
        SqlMapper.AddTypeHandler(new DateTimeHandler());
    }
}