namespace ToDoTests;

public class UnitTest1
{
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
    public void UpdatesOnClientShowUpOnServer()
    {
        // Arrange
        

        // Act

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