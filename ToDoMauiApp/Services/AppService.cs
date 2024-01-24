using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public async Task Add(string todo)
    {
        // CALL CONTROLLER YAY
        await client.GetStringAsync(todo);
    }
}
