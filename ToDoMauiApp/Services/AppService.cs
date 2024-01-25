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
    public AppService()
    {
        client = new HttpClient();
    }

    public async Task AddTodo(string todo)
    {
        ToDo todoObject = new ToDo() { Text = todo };   
        await client.PostAsJsonAsync<ToDo>($"http://localhost:5223/{todo}", todoObject);
    }

    public async Task DeleteTodo(ToDo todo)
    {
        await client.DeleteFromJsonAsync<ToDo>($"http://localhost:5223/{todo}");
    }

    public async Task<List<ToDo>> GetAllTodos()
    {
       return await client.GetFromJsonAsync<List<ToDo>>($"http://localhost:5223/getall");
    }

    public async Task UpdateTodo(ToDo t, string todo)
    {
        await client.PatchAsJsonAsync($"http://localhost:5223/{t}/{todo}", t);
    }
}
