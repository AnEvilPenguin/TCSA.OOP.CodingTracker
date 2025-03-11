using System.Data.SQLite;
using Dapper;
using TCSA.OOP.CodingTracker;

await using var connection = new SQLiteConnection("Data Source=tracker.db");