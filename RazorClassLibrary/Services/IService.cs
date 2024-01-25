using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RazorClassLibrary.Services;
using RazorClassLibrary.Data;

public interface IService
{
    public Task AddTodo(string todo);

    public Task DeleteTodo(ToDo todo);

    public Task UpdateTodo(ToDo t, string todo);

    public Task<List<ToDo>> GetAllTodos();
}
