using RazorClassLibrary.Data;
using SQLite;

namespace ToDoTests;

public class DatabaseInitialize
{
    private SQLiteAsyncConnection conn;

    private string _dbPath = "";

    public DatabaseInitialize(string databaseName)
    {
        _dbPath = databaseName;
    }

    private async Task Init()
    {
        if (conn is not null)
            return;

        conn = new SQLiteAsyncConnection(_dbPath);
        await conn.CreateTableAsync<ToDo>();
    }
}