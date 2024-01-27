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
    public HttpClient client;
    public ToDoRepository repo { get; set; }

    public AppService()
    {
        client = new HttpClient();
        repo = new ToDoRepository();
    }

    public async Task AddTodo(string todo, bool access)
    {


        if (access)
        {
            ToDo mweep = new() { Text = todo };
            await client.PostAsJsonAsync<ToDo>($"http://localhost:5289/{todo}", mweep);
        }
        else
        {
            await repo.AddTodo(todo, access);
        }
    }

    public async Task DeleteTodo(int todo, bool access)
    {

        if (access)
        {
            try
            {
                var response = await client.DeleteAsync($"http://localhost:5289/delete/{todo}");

                // Check if the request was successful (HTTP status code 2xx)
                response.EnsureSuccessStatusCode();

                // No need to parse response content as there might not be any
            }
            catch (HttpRequestException ex)
            {
                // Handle exception
            }
        }
        else
        {
            await repo.DeleteTodo(todo, access);
        }
    }

    public async Task<List<ToDo>> GetAllTodos(bool access)
    {
        List<ToDo> onlineItems = new();
        List<ToDo> todos = new();

        if (access)
        {
            onlineItems = await client.GetFromJsonAsync<List<ToDo>>($"http://localhost:5289/getall");
            List<ToDo> localItems = await repo.GetAllTodos(access);

            return onlineItems;
        }
        else
        {
            todos = await repo.GetAllTodos(access);
            return todos;
        }
    }

    public async Task UpdateTodo(ToDo t, string todo, bool access)
    {

        if (access)
        {
            await client.PatchAsJsonAsync<ToDo>($"http://localhost:5289/update/{t}/{todo}", t);
        }
        else
        {
            await repo.UpdateTodo(t, todo, access);
        }
    }

    //sync from local to online
    public async Task SyncDbs()
    {
        client = new HttpClient();
        List<ToDo> localToDos = await GetAllTodos(true);

        // call api for all onlines
        List<ToDo> onlineToDos = await client.GetFromJsonAsync<List<ToDo>>($"http://localhost:5289/getall");
        List<ToDo> tempToDos = new();
        bool exists = false;


        foreach (ToDo toDo in localToDos)
        {
            exists = false;
            foreach (ToDo t in onlineToDos)
            {
                if (t.Text == toDo.Text) { exists = true; }
            }
            if(!exists)
            {
                tempToDos.Add(toDo);
                ToDo mweep = new() { Text = toDo.Text };
                await client.PostAsJsonAsync<ToDo>($"http://localhost:5289/{toDo.Text}", mweep);
            }
        }

        //sync online to local at the same time
        {
            foreach (ToDo toDo in onlineToDos)
            {
                exists = false;
                foreach (ToDo t in localToDos)
                {
                    if (t.Text == toDo.Text) { exists = true; }
                }
                if (!exists)
                {
                    tempToDos.Add(toDo);

                }
            }

            foreach (ToDo toDo in tempToDos)
            {
                await repo.AddTodo(toDo.Text, false);
            }
        }
    }
}
