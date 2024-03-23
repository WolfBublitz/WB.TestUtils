using System;

namespace ConsoleOutputGrabberTests.GrabOutputTest;

[TestClass]
public class TheGrabOutputMethod
{
    [TestMethod]
    public void ShallGrabTheOutput()
    {
        // arrange
        ConsoleOutputGrabber grabber = new();

        // act
        using (IDisposable disposer = grabber.Start())
        {
            Console.WriteLine("Hello, World!");
        }

        // assert
        grabber.Output.Should().Be("Hello, World!" + Environment.NewLine);
    }
}
