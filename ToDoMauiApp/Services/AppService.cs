using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using RazorClassLibrary.Data;
using RazorClassLibrary.Services;

namespace ToDoMauiApp.Services;

// MAUI APP SERVICE 
public class AppService : IService
{
    private HttpClient client;
    public ToDoRepository repo { get; set; }

    public AppService()
    {
        client = new HttpClient();
        repo = new ToDoRepository();
    }

    public async Task AddTodo(string todo)
    {
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        if (accessType == NetworkAccess.Internet)
        {
            ToDo mweep = new();
            await client.PostAsJsonAsync<ToDo>($"http://localhost:5289/{todo}", mweep);
        }
        else
        {
            await repo.AddTodo(todo);
            todo = string.Empty;
        }
    }

    public async Task DeleteTodo(ToDo todo)
    {
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        if (accessType == NetworkAccess.Internet)
        {
            var response = await client.DeleteFromJsonAsync<ToDo>($"http://localhost:5289/delete/{todo}");
        }
        await repo.DeleteTodo(todo);
    }

    public async Task<List<ToDo>> GetAllTodos()
    {
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;
        List<ToDo> onlineItems = new();
        List<ToDo> todos = new();

        if (accessType == NetworkAccess.Internet)
        {
            onlineItems = await client.GetFromJsonAsync<List<ToDo>>($"http://localhost:5289/getall");
            List<ToDo> localItems = await repo.GetAllTodos();
            foreach (var item in onlineItems)
            {
                if (!localItems.Contains(item))
                {
                    localItems.Add(item);
                }
            }
            foreach (var item in localItems)
            {
                if (!onlineItems.Contains(item))
                {
                    onlineItems.Add(item);
                }
            }
            return onlineItems;
        }
        else
        {
            todos = await repo.GetAllTodos();
            return todos;
        }
    }

    public async Task UpdateTodo(ToDo t, string todo)
    {
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        if (accessType == NetworkAccess.Internet)
        {
            await client.PatchAsJsonAsync<ToDo>($"http://localhost:5289/update/{t}/{todo}", t);
            //TODO MAKE SYNC
        }
        else
        {
            await repo.UpdateTodo(t, todo);
        }
    }
}
