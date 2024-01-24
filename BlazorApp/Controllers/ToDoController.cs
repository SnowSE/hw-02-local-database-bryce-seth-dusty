using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RazorClassLibrary.Services;

namespace BlazorApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToDoController(IService service) : ControllerBase
{
    // TODO calls methods in the BlazorServerApp service
    [HttpPost("")]
    public async Task AddTodoItem(string item)
    {
        await service.Add(item);
    }
}
