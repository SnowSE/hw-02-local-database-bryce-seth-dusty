
using RazorClassLibrary.Services;
using SQLite;
using ToDoMauiApp;
using RazorClassLibrary.Data;

namespace BlazorApp.Services;

public class WebAppService : IService
{
    private HttpClient client;
    public WebAppService()
    {
        client = new HttpClient();
    }

    public async Task AddTodo(string todo, bool hey)
    {
        ToDo todoObject = new ToDo() { Text = todo };
        await client.PostAsJsonAsync<ToDo>($"http://localhost:5223/{todo}", todoObject);
    }

    public async Task DeleteTodo(int todo, bool hey)
    {
        await client.DeleteFromJsonAsync<ToDo>($"http://localhost:5223/{todo}");
    }

    public async Task<List<ToDo>> GetAllTodos(bool hey)
    {
        return await client.GetFromJsonAsync<List<ToDo>>($"http://localhost:5223/getall");
    }

    public Task SyncDbs()
    {
        throw new NotImplementedException();
    }

    public async Task UpdateTodo(ToDo t, string todo, bool hey)
    {
        await client.PatchAsJsonAsync($"http://localhost:5223/{t}/{todo}", t);
    }
}
