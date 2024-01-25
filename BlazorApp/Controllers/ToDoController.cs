using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorClassLibrary.Services;
using BlazorApp.Services;
using RazorClassLibrary.Data;

namespace BlazorApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToDoController(IService service) : ControllerBase
{
    // TODO calls methods in the BlazorServerApp service
    [HttpPost("")]
    public async Task AddTodoItem(string item)
    {
        await service.AddTodo(item);
    }

    [HttpGet("/getall")]
    public async Task GetAll()
    {
        await service.GetAllTodos();
    }

    [HttpDelete("{id}")]
    public async Task Delete(ToDo todo)
    {
        await service.DeleteTodo(todo);
    }

    [HttpPatch("{id}")]
    public async Task Update(int id, string NewText)
    {

    }
}
