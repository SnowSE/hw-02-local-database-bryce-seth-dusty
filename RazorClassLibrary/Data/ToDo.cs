using SQLite;

namespace RazorClassLibrary.Data
{

    [Table("todos")]
    public class ToDo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(600)]
        public string Text { get; set; }
    }
}
