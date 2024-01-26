using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorClassLibrary.Data;
using RazorClassLibrary.Services;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToDoController : ControllerBase
{
    private readonly IService service;
    private readonly ILogger<ToDoController> logger;
    public ToDoController(IService _service, ILogger<ToDoController> _logger)
    {
        this.service = _service;
        logger = _logger;
    }

    // TODO calls methods in the BlazorServerApp service
    [HttpPost("/{item}")]
    public async Task AddTodoItem(string item)
    {
        logger.LogInformation("Adding an item with the following text: " + item);
        await service.AddTodo(item);
    }

    [HttpGet("/getall")]
    public async Task<List<ToDo>> GetAll()
    {
        return await service.GetAllTodos();
    }

    [HttpDelete("/delete/{todo}")]
    public async Task Delete(ToDo todo)
    {
        await service.DeleteTodo(todo);
    }

    [HttpPatch("/update/{todo}/{NewText}")]
    public async Task Update([FromBody] ToDo todo, string NewText)
    {
        await service.UpdateTodo(todo, NewText);
    }
}
