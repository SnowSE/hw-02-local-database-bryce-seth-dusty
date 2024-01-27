using RazorClassLibrary.Data;
using RazorClassLibrary.Services;
using SQLite;

namespace WebAPI;

public class OnlineToDoRepository : IService
{

    public HttpClient client { get; set; }
    public string StatusMessage { get; set; }

    // TODO: Add variable for the SQLite connection
    private SQLiteAsyncConnection conn;

    private string _dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Onlinetodos.db3");

    private async Task Init()
    {
        // TODO: Add code to initialize the repository
        if (conn is not null)
            return;

        conn = new SQLiteAsyncConnection(_dbPath);
        await conn.CreateTableAsync<ToDo>();
    }


    public async Task AddTodo(string text)
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
            result = await conn.InsertAsync(new ToDo { Text = text });

            StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, text);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", text, ex.Message);
        }

    }

    public async Task<List<ToDo>> GetAllTodos()
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

    public async Task DeleteTodo(int todoId)
    {
        await Init();

        foreach(ToDo todo in await GetAllTodos())
        {
            if (todo.Id == todoId)
                await conn.DeleteAsync(todo);
        }

    }

    public async Task UpdateTodo(ToDo toDo, string NewText)
    {
        await Init();
        toDo.Text = NewText;

        await conn.UpdateAsync(toDo);
    }

    public async Task SyncDbs()
    {
        client = new HttpClient();
        List<ToDo> localToDos = await GetAllTodos();

        // call api for all onlines
        List<ToDo> onlineToDos = await client.GetFromJsonAsync<List<ToDo>>($"http://localhost:5223/getall");


        foreach (ToDo toDo in onlineToDos)
        {
            if (!localToDos.Contains(toDo))
            {
                await AddTodo(toDo.Text);
            }
        }
    }
}
