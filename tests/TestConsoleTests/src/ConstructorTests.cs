namespace TestConsoleTests.TheConstructorTests;

[TestClass]
public class TheConstructor
{
    [TestMethod]
    public void SholdProvideASingleLineOfFakeInput()
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

    [TestMethod]
    public void SholdProvideMultipleLinesOfFakeInput()
    {
        // Arrange
        string[] input = ["One", "Two", "Three"];
        string? outputLine1;
        string? outputLine2;
        string? outputLine3;

        // Act
        using (TestConsole console = new(input))
        {
            outputLine1 = console.ReadLine();
            outputLine2 = console.ReadLine();
            outputLine3 = console.ReadLine();
        };

        // Assert
        outputLine1.Should().Be(input[0]);
        outputLine2.Should().Be(input[1]);
        outputLine3.Should().Be(input[2]);
    }
}
