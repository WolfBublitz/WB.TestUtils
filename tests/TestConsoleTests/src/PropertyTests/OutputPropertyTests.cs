using System;

namespace TestConsoleTests.PropertyTests.OutputPropertyTests;

[TestClass]
public class TheOutputProperty
{
    [TestMethod]
    public void ShouldProvideTheRecordedOutput()
    {
        // Arrange
        const string testOutput = "Hello, World!";
        string recordedOutput;

        // Act
        using (TestConsole console = new())
        {
            System.Console.WriteLine(testOutput);

            recordedOutput = console.Output;
        };

        // Assert
        recordedOutput.Should().Be(testOutput + Environment.NewLine);
    }

    [TestMethod]
    public void ShouldBeEmptyAtDefault()
    {
        // Arrange
        string recordedOutput;

        // Act
        using (TestConsole console = new())
        {
            recordedOutput = console.Output;
        };

        // Assert
        recordedOutput.Should().BeEmpty();
    }
}
