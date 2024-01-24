using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ToDoMauiApp.Models;

[Table("todos")]
public class ToDo
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [MaxLength(600)]
    public string Text { get; set; }
}
