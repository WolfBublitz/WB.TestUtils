using System;
using System.Collections.Generic;
using System.Linq;

namespace TestConsoleTests.PropertyTests.LinesPropertyTests;

[TestClass]
public class TheLinesProperty
{
    [TestMethod]
    public void ShouldBeEmptyAtDefault()
    {
        // Arrange
        IEnumerable<string> recordedLines;

        // Act
        using (TestConsole console = new())
        {
            recordedLines = console.Lines.ToArray();
        };

        // Assert
        recordedLines.Should().BeEmpty();
    }

    [TestMethod]
    public void ShouldProvideTheRecordedOutputAsListOfLines()
    {
        // Arrange
        string[] testOutput = ["Hello, World!", "Hello, Universe!"];
        IEnumerable<string> recordedLines;

        // Act
        using (TestConsole console = new())
        {
            foreach (string line in testOutput)
            {
                System.Console.WriteLine(line);
            }

            recordedLines = console.Lines.ToArray();
        };

        // Assert
        recordedLines.Should().BeEquivalentTo(testOutput);
    }
}
