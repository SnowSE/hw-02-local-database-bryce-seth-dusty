using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RazorClassLibrary.Services;
using RazorClassLibrary.Data;

public interface IService
{
    public Task AddTodo(string todo, bool access);

    public Task DeleteTodo(int todo, bool access);

    public Task UpdateTodo(ToDo t, string todo, bool access);

    public Task<List<ToDo>> GetAllTodos(bool access);

    public Task SyncDbs();
}
