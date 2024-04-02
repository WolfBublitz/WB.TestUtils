namespace TestConsoleTests.TheConstructorTests;

[TestClass]
public class TheConstructor
{
    [TestMethod]
    public void SholdProvideFakeInput()
    {
        // Arrange
        const string input = "Hello, World!";
        string? output;

        // Act
        using (TestConsole console = new(input))
        {
            output = console.ReadLine();
        };

        // Assert
        output.Should().Be(input);
    }
}
