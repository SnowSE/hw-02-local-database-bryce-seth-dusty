using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorClassLibrary.Data;
using RazorClassLibrary.Services;

namespace ToDoMauiApp.Services;

// MAUI APP SERVICE 
public class AppService : IService
{
    private HttpClient client;
    public AppService()
    {
        client = new HttpClient();
    }

    public Task AddTodo(string todo)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTodo(ToDo todo)
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> GetAllTodos()
    {
        throw new NotImplementedException();
    }

    public Task UpdateTodo(ToDo t, string todo)
    {
        throw new NotImplementedException();
    }
}
