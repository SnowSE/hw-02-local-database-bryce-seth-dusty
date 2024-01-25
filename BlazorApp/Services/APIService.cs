
using RazorClassLibrary.Services;
using SQLite;
using ToDoMauiApp;
using RazorClassLibrary.Data;

namespace BlazorApp.Services;

public class APIService(OnlineToDoRepository repo) : IService
{
    public OnlineToDoRepository repo { get; set; }
    public async Task AddTodo(string todo)
    {
        //var conn = new SQLiteAsyncConnection(dbpath);
        
        // TODO CONNECT TO SQL DATABASE!!!! YAY
        //throw new NotImplementedException();
        await repo.AddNewToDo(todo);
    }
    
    public async Task UpdateTodo(ToDo todo, string NewText)
    {
        await repo.UpdateTodo(todo, NewText);
    }

    public async Task<List<ToDo>> GetAllTodos()
    {
        return await repo.GetAllTodos();
    }

    public async Task DeleteTodo(ToDo todo)
    {
        await repo.DeleteTodo(todo.Id);
    }

    Task<List<string>> IService.GetAllTodos()
    {
        throw new NotImplementedException();
    }
}
