using System.Collections.Generic;
using System.Linq;

namespace TestConsoleTests.PropertyTests.LastLinePropertyTests;

[TestClass]
public class TheLastLinesProperty
{
    [TestMethod]
    public void ShouldBeEmptyAtDefault()
    {
        // Arrange
        string? recordedLastLine;

        // Act
        using (TestConsole console = new())
        {
            recordedLastLine = console.LastLine;
        };

        // Assert
        recordedLastLine.Should().BeNull();
    }

    [TestMethod]
    public void ShouldProvideTheRecordedOutputAsListOfLines()
    {
        // Arrange
        string[] testOutput = ["Hello, World!", "Hello, Universe!"];
        string? recordedLastLine;

        // Act
        using (TestConsole console = new())
        {
            foreach (string line in testOutput)
            {
                System.Console.WriteLine(line);
            }

            recordedLastLine = console.LastLine;
        };

        // Assert
        recordedLastLine.Should().Be("Hello, Universe!");
    }
}
