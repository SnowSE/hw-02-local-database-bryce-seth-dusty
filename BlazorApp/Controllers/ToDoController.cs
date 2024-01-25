using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorClassLibrary.Services;
using BlazorApp.Services;
using RazorClassLibrary.Data;

namespace BlazorApp.Controllers;

[Route("[controller]")]
[ApiController]
public class ToDoController(IService service) : ControllerBase
{
    // TODO calls methods in the BlazorServerApp service
    [HttpPost("/{item}")]
    public async Task AddTodoItem(string item)
    {
        await service.AddTodo(item);
    }

    [HttpGet("/getall")]
    public async Task GetAll()
    {
        await service.GetAllTodos();
    }

    [HttpDelete("/{todo}")]
    public async Task Delete([FromBody] ToDo todo)
    {
        await service.DeleteTodo(todo);
    }

    [HttpPatch("{todo}/{NewText}")]
    public async Task Update([FromBody] ToDo todo, string NewText)
    {
        await service.UpdateTodo(todo, NewText);
    }
}
