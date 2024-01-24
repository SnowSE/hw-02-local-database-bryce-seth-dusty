using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoMauiApp.Models;
using SQLite;

namespace ToDoMauiApp;

public class ToDoRepository
{
    string _dbPath;;

    public string StatusMessage { get; set; }

    // TODO: Add variable for the SQLite connection
    private SQLiteConnection conn;

    private void Init()
    {
        // TODO: Add code to initialize the repository
        if (conn is not null)
            return;
        
        conn = new SQLiteConnection(_dbPath);
        conn.CreateTable<ToDo>();
    }

    public ToDoRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    public void AddNewToDo(string text)
    {
        ArgumentNullException.ThrowIfNull(text, nameof(text));
        int result = 0;
        try
        {
            // TODO: Call Init()
            Init();

            // basic validation to ensure a name was entered
            if (string.IsNullOrEmpty(text))
                throw new Exception("Valid text required");

            // TODO: Insert the new person into the database
            result = conn.Insert( new ToDo { Text = text } );

            StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, text);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", text, ex.Message);
        }

    }

    public List<ToDo> GetAllTodos()
    {
        // TODO: Init then retrieve a list of Person objects from the database into a list
        try
        {
            Init(); 

            return conn.Table<ToDo>().ToList(); 
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<ToDo>();
    }
}
