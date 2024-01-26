using Moq;
using RazorClassLibrary.Data;
using RazorClassLibrary.Services;
using ToDoMauiApp;
using ToDoMauiApp.Services;

namespace ToDoTests;

public class UnitTest1
{
    private readonly AppService _sut;
    private readonly Mock<ToDoRepository> _todoRepoMock = new Mock<ToDoRepository>();
    //private readonly Mock<IService> _logger = new Mock<IService>();

    public UnitTest1()
    {
        _sut = new AppService();
    }

    [Fact]
    public void PassingTest()
    {
        Assert.Equal(5, 5);
    }

    [Fact]
    public void FailingTest()
    {
        Assert.Equal(5, 1);
    }

    [Fact]
    public async Task GetSomeTodo()
    {
        // Arrange
        var todos = await _sut.GetAllTodos();

        // Act
        bool greaterThanZero = todos.Count > 0;

        // Assert
        Assert.True(greaterThanZero);
    }

    [Fact]
    public async void UpdatesOnClientShowUpOnServer()
    {
        // Arrange
        

        // Act
        var allTodos = await _sut.GetAllTodos();

        // Assert
    }

    [Fact]
    public void UpdatesOnServerShowUpOnClient()
    {
        Assert.Equal(5, 1);
    }

    [Fact]
    public void LastWriteWins()
    {
        Assert.Equal(5, 1);
    }
}