using System;
using System.Collections.Generic;
using System.Linq;

namespace TestConsoleTests.MethodTests.LineAtMethodTests;

[TestClass]
public class TheLastLinesProperty
{
    [TestMethod]
    public void ShouldProvideTheRecordedOutputAsListOfLines()
    {
        // Arrange
        string[] testOutput = ["Hello, World!", "Hello, Universe!"];
        string line1;
        string line2;

        // Act
        using (TestConsole console = new())
        {
            foreach (string line in testOutput)
            {
                System.Console.WriteLine(line);
            }

            line1 = console.LineAt(0);
            line2 = console.LineAt(1);
        };

        // Assert
        line1.Should().Be("Hello, World!");
        line2.Should().Be("Hello, Universe!");
    }

    [TestMethod]
    public void ShouldThrowIndexOutOfRangeException()
    {
        // Arrange
        string[] testOutput = ["Hello, World!", "Hello, Universe!"];

        // Act
        using (TestConsole console = new())
        {
            foreach (string line in testOutput)
            {
                System.Console.WriteLine(line);
            }

            // Assert
            Action action = () => console.LineAt(2);
            action.Should().Throw<ArgumentOutOfRangeException>();
        };
    }
}
