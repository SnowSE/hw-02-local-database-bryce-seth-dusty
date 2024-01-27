using Moq;
using RazorClassLibrary.Data;
using RazorClassLibrary.Services;
using System.Net.NetworkInformation;
using ToDoMauiApp;
using ToDoMauiApp.Services;

namespace ToDoTests;

public class UnitTest1 : IClassFixture<MyWebAppFactory>
{

    public HttpClient _httpClient { get; set; }
    public MyWebAppFactory appFactory { get; set; }
    public AppService _sut;
    private Mock<ToDoRepository> _todoRepoMock = new Mock<ToDoRepository>();
    private static string FilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    //private readonly Mock<IService> _logger = new Mock<IService>();

    public UnitTest1(MyWebAppFactory factory)
    {
        _httpClient = factory.CreateDefaultClient();
        appFactory = factory;
        _sut = new AppService();
    }

    [Fact]
    public async Task GetSomeTodosWhileOnline()
    {
        // Arrange
        var todos = await _sut.GetAllTodos(true);

        // Act
        bool greaterThanZero = todos.Count > 0;

        // Assert
        Assert.True(greaterThanZero);
    }

    [Fact]
    public async Task GetSomeTodosWhileOffline()
    {
        // Arrange
        var todos = await _sut.GetAllTodos(false);

        // Act
        bool greaterThanZero = todos.Count > 0;

        // Assert
        Assert.True(greaterThanZero);
    }


    [Fact]
    public async void UpdatesOnClientShowUpOnServer()
    {
        // Arrange
        string NewToDo =  "This is added offline";
        await _sut.SyncDbs();

        // Act
        await _sut.AddTodo(NewToDo, false);                     // add the todo OFFLINE

        var allOnlineTodos = await _sut.GetAllTodos(true);
        var allLocalTodos = await _sut.GetAllTodos(true);
        int numOnlineTodos = allOnlineTodos.Count;
        int numLocalTodos = allLocalTodos.Count;

        await _sut.SyncDbs();      // sync databases

        // Assert
        Assert.Equal(numOnlineTodos, numLocalTodos);
    }

    [Fact]
    public async void MultipleUpdatesOnClientShowUpOnServer()
    {
        // Arrange
        string NewToDo = "This is added offline";
        string NewToDo2 = "This is another added offline";
        await _sut.SyncDbs();

        // Act
        await _sut.AddTodo(NewToDo, false);                     // add the todo OFFLINE
        await _sut.AddTodo(NewToDo2, false);                     // add the todo OFFLINE

        var allOnlineTodos = await _sut.GetAllTodos(true);
        var allLocalTodos = await _sut.GetAllTodos(true);
        int numOnlineTodos = allOnlineTodos.Count;
        int numLocalTodos = allLocalTodos.Count;

        await _sut.SyncDbs();      // sync databases

        // Assert
        Assert.Equal(numOnlineTodos, numLocalTodos);
    }

    [Fact]
    public async void UpdatesOnServerShowUpOnClient()
    {
        // Arrange
        string NewToDo = "This is added online!";
        await _sut.SyncDbs();

        // Act
        await _sut.AddTodo(NewToDo, true);                     // add the todo ONLINE

        var allOnlineTodos = await _sut.GetAllTodos(true);
        var allLocalTodos = await _sut.GetAllTodos(true);
        int numOnlineTodos = allOnlineTodos.Count;
        int numLocalTodos = allLocalTodos.Count;

        await _sut.SyncDbs();                                   // sync databases

        // Assert
        Assert.Equal(numOnlineTodos, numLocalTodos);
    }

    [Fact]
    public async void MultipleUpdatesOnServerShowUpOnClient()
    {
        // Arrange
        string NewToDo = "This is added online!";
        string NewToDo2 = "This is ANOTHER added online!";
        await _sut.SyncDbs();

        // Act
        await _sut.AddTodo(NewToDo, true);                     // add the todo ONLINE
        await _sut.AddTodo(NewToDo2, true);                     // add the todo ONLINE

        var allOnlineTodos = await _sut.GetAllTodos(true);
        var allLocalTodos = await _sut.GetAllTodos(true);
        int numOnlineTodos = allOnlineTodos.Count;
        int numLocalTodos = allLocalTodos.Count;

        await _sut.SyncDbs();                                   // sync databases

        // Assert
        Assert.Equal(numOnlineTodos, numLocalTodos);
    }

    [Fact]
    public async void OfflineAdditionsAreNotRecordedInOnlineDbWithoutSyncing()
    {
        await _sut.SyncDbs();

        // Arrange
        string NewToDo = "This is added offline!";

        // Act
        await _sut.AddTodo(NewToDo, false);                     // add the todo OFFLINE

        var allOnlineTodos = await _sut.GetAllTodos(true);
        var allLocalTodos = await _sut.GetAllTodos(false);
        int numOnlineTodos = allOnlineTodos.Count;
        int numLocalTodos = allLocalTodos.Count;

        // Assert
        Assert.NotEqual(numOnlineTodos, numLocalTodos);
    }

    [Fact]
    public async void OnlineAdditionsAreNotRecordedInOfflineDbWithoutSyncing()
    {
        await _sut.SyncDbs();

        // Arrange
        string NewToDo = "This is added ONLINE and without syncing!";

        // Act
        await _sut.AddTodo(NewToDo, true);                     // add the todo ONLINE

        var allOnlineTodos = await _sut.GetAllTodos(false);
        var allLocalTodos = await _sut.GetAllTodos(true);
        int numOnlineTodos = allOnlineTodos.Count;
        int numLocalTodos = allLocalTodos.Count;

        // Assert
        Assert.NotEqual(numOnlineTodos, numLocalTodos);
    }


}