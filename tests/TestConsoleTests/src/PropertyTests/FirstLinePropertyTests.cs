using System.Collections.Generic;
using System.Linq;

namespace TestConsoleTests.PropertyTests.FirstLinePropertyTests;

[TestClass]
public class TheFirstLinesProperty
{
    [TestMethod]
    public void ShouldBeEmptyAtDefault()
    {
        // Arrange
        string? recordedFirstLine;

        // Act
        using (TestConsole console = new())
        {
            recordedFirstLine = console.FirstLine;
        };

        // Assert
        recordedFirstLine.Should().BeNull();
    }

    [TestMethod]
    public void ShouldProvideTheRecordedOutputAsListOfLines()
    {
        // Arrange
        string[] testOutput = ["Hello, World!", "Hello, Universe!"];
        string? recordedFirstLine;

        // Act
        using (TestConsole console = new())
        {
            foreach (string line in testOutput)
            {
                System.Console.WriteLine(line);
            }

            recordedFirstLine = console.FirstLine;
        };

        // Assert
        recordedFirstLine.Should().Be("Hello, World!");
    }
}
