# WB.TestUtils

A collection of utilities for writing tests in C#.

## Installation

```bash
dotnet add package WB.TestUtils
```

## Features

### Scrubbers

A set of extension methods for scrubbing dynamic data from strings. Currently the following scrubbers are available:

- `Scrub`: A general purpose scrubber that replaces all occurrences of a given string with a specified replacement.
- `ScrubDateTime`: A scrubber that replaces all occurrences of a date and time with a specified replacement.

### TestConsole

A utility for testing console output. The `TestConsole` class provides a way to simulate user input and capture console output in a test environment. It is especially useful for testing console applications that require user input.

#### Recording Console Output

The following example demonstrates how to capture console output using the `TestConsole` class:

```c#
using WB.TestUtils;

string? output;

using (TestConsole console = new())
{
    Console.WriteLine("Hello, world!");
    Console.WriteLine("This is a test.");
    Console.WriteLine("Goodbye!");

    output = console.Output;
}

output.Should().Be("Hello, world!\nThis is a test.\nGoodbye!\n");
```

#### Simulating User Input

The following example demonstrates how to simulate user input using the `TestConsole` class:

```c#
using WB.TestUtils;

string[] input = { "yes", "no" };

string? response1;
string? response2;

using (TestConsole console = new(input))
{
    response1 = console.ReadLine();
    response2 = console.ReadLine();
}

response1.Should().Be("yes");
response2.Should().Be("no");
```
