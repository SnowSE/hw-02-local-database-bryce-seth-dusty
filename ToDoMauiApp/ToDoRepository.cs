﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorClassLibrary.Data;
using SQLite;
using Dotmim.Sync.SqlServer;
using Dotmim.Sync.Sqlite;
using Dotmim.Sync;
using RazorClassLibrary.Services;

namespace ToDoMauiApp;

public class ToDoRepository : IService
{
    public string StatusMessage { get; set; }

    // TODO: Add variable for the SQLite connection
    private SQLiteAsyncConnection conn;

    private string _dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Localtodos.db3");

    private async Task Init()
    {
        // TODO: Add code to initialize the repository
        if (conn is not null)
            return;
        
        conn = new SQLiteAsyncConnection(_dbPath);
        await conn.CreateTableAsync<ToDo>();
    }

    public async Task AddTodo(string text, bool hey)
    {
        ArgumentNullException.ThrowIfNull(text, nameof(text));
        int result = 0;
        try
        {
            // TODO: Call Init()
            await Init();

            // basic validation to ensure a name was entered
            if (string.IsNullOrEmpty(text))
                throw new Exception("Valid text required");

            // TODO: Insert the new person into the database
            result = await conn.InsertAsync( new ToDo { Text = text } );

            StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, text);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", text, ex.Message);
        }

    }

    public async Task<List<ToDo>> GetAllTodos(bool hey)
    {
        // TODO: Init then retrieve a list of Person objects from the database into a list
        try
        {
            await Init(); 

            return await conn.Table<ToDo>().ToListAsync(); 
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<ToDo>();
    }

    public async Task DeleteTodo(int todoId, bool hey)
    {
        await Init();

        foreach (ToDo todo in await GetAllTodos(false))
        {
            if (todo.Id == todoId)
                await conn.DeleteAsync(todo);
        }
    }

    public async Task UpdateTodo(ToDo toDo, string NewText, bool hey)
    {
        await Init();
        toDo.Text = NewText;

        await conn.UpdateAsync(toDo);
    }

    public async Task SyncDbs()
    {
       
    }

}
