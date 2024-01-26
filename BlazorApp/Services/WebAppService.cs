
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
