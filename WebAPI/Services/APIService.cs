using RazorClassLibrary.Data;
using RazorClassLibrary.Services;

namespace WebAPI.Services;

// This is the service that talks to the online database
public class APIService : IService
{
    public APIService()
    {
        repo = new OnlineToDoRepository();
    }

    public OnlineToDoRepository repo { get; set; }

    public async Task AddTodo(string todo)
    {
        await repo.AddTodo(todo);
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
        await repo.DeleteTodo(todo);
    }

}
