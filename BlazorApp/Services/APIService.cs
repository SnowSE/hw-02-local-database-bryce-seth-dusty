
using RazorClassLibrary.Services;
using SQLite;
using ToDoMauiApp;
using RazorClassLibrary.Data;

namespace BlazorApp.Services;

public class APIService: IService
{
    public APIService()
    {
        repo = new OnlineToDoRepository("database");
    }

    public OnlineToDoRepository repo { get; set; }

    public async Task AddTodo(string todo)
    {
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

    Task<List<ToDo>> IService.GetAllTodos()
    {
        throw new NotImplementedException();
    }

}
