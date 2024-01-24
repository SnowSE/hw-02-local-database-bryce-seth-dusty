namespace ToDoMauiApp
{
    public partial class App : Application
    {
        public static ToDoRepository ToDoRepo { get; private set; } 
        public App(ToDoRepository repo)
        {
            InitializeComponent();

            MainPage = new MainPage();

            ToDoRepo = repo;
        }
    }
}
