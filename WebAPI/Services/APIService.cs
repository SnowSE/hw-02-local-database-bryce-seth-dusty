using RazorClassLibrary.Data;
using RazorClassLibrary.Services;

namespace WebAPI.Services;

// This is the service that talks to the online database
public class APIService : IService
{
    public HttpClient client;
    public APIService()
    {
        repo = new OnlineToDoRepository();
    }

    public OnlineToDoRepository repo { get; set; }

    public async Task AddTodo(string todo, bool isOnline)
    {
        await repo.AddTodo(todo, isOnline);
    }

    public async Task UpdateTodo(ToDo todo, string NewText, bool isOnline)
    {
        await repo.UpdateTodo(todo, NewText, isOnline);
    }

    public async Task<List<ToDo>> GetAllTodos(bool isOnline)
    {
        return await repo.GetAllTodos(isOnline);
    }

    public async Task DeleteTodo(int todoId, bool isOnline)
    {
        await repo.DeleteTodo(todoId, isOnline);
    }

    //sync from online to local
    public async Task SyncDbs()
    {

    }

}
