// ----------------------------------------------------------------------------
// <copyright file="TestConsole.cs" company="WB">
// Copyright (c) WB Project
// </copyright>
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WB.TestUtils.Console;

/// <summary>
/// Represents a test console that can be used to capture the console output and fake the console input
/// for testing purposes.
/// </summary>
/// <example>
/// The following example demonstrates how to use the <see cref="ConsoleOutputGrabber"/> class to grab the
/// console output.
/// <code>
/// using System;
/// using WB.TestUtils.Console;
///
/// string recordedOutput;
///
/// using (TestConsole testConsole = new())
/// {
///     Console.WriteLine("Hello, World!");
///
///     recordedOutput = testConsole.Output;
/// }
/// </code>
/// </example>
public sealed class TestConsole : IDisposable
{
    // ┌────────────────────────────────────────────────────────────────────────────────┐
    // │ Private Fields                                                                 │
    // └────────────────────────────────────────────────────────────────────────────────┘
    private readonly TextReader defaultInput;

    private readonly TextWriter defaultOutput;

    private readonly StringReader input;

    private readonly StringWriter output = new();

    // ┌────────────────────────────────────────────────────────────────────────────────┐
    // │ Public Constructors                                                            │
    // └────────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Initializes a new instance of the <see cref="TestConsole"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor initializes a new instance of the <see cref="TestConsole"/> with
    /// optional input. Each string in the <paramref name="input"/> array is treated as a
    /// separate line of input.
    /// </remarks>
    /// <param name="input">The input to provide to the console.</param>
    public TestConsole(params string[] input)
    {
        defaultInput = System.Console.In;
        defaultOutput = System.Console.Out;

        this.input = new StringReader(string.Join(Environment.NewLine, input));

        System.Console.SetIn(this.input);
        System.Console.SetOut(output);
    }

    /// <summary>
    /// Gets the output of the console.
    /// </summary>
    public string Output => output.ToString();

    /// <summary>
    /// Gets the output of the console as a collection of lines.
    /// </summary>
    public IEnumerable<string> Lines
    {
        get
        {
            string trimmed = output.ToString().Trim();

            if (trimmed.Length == 0)
            {
                return Array.Empty<string>();
            }
            else
            {
                return trimmed.Split(Environment.NewLine);
            }
        }
    }

    /// <summary>
    /// Gets the first line of the console output.
    /// </summary>
    public string? FirstLine => Lines.FirstOrDefault();

    /// <summary>
    /// Gets the last line of the console output.
    /// </summary>
    public string? LastLine => Lines.LastOrDefault();

    // ┌────────────────────────────────────────────────────────────────────────────────┐
    // │ Public Methods                                                                 │
    // └────────────────────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Returns the line at the specified <paramref name="index"/>.
    /// </summary>
    /// <param name="index">The index of the line to get.</param>
    /// <returns>The line at the specified <paramref name="index"/>.</returns>
    public string LineAt(int index) => Lines.ElementAt(index);

    /// <summary>
    /// Reads the next line of characters from the input stream.
    /// </summary>
    /// <returns>The next line of characters from the input stream, or <see langword="null"/> if no more lines are available.</returns>
#pragma warning disable CA1822 // Member als statisch markieren
    public string? ReadLine() => System.Console.ReadLine();
#pragma warning restore CA1822 // Member als statisch markieren

    /// <summary>
    /// Writes the specified value to the console.
    /// </summary>
    /// <param name="value">The value to write.</param>
#pragma warning disable CA1822 // Member als statisch markieren
    public void Write(string value) => System.Console.Write(value);
#pragma warning restore CA1822 // Member als statisch markieren

    /// <summary>
    /// Writes the specified value to the console followed by the newline line terminator.
    /// </summary>
    /// <param name="value">The value to write.</param>
#pragma warning disable CA1822 // Member als statisch markieren
    public void WriteLine(string value) => System.Console.WriteLine(value);
#pragma warning restore CA1822 // Member als statisch markieren

    /// <inheritdoc/>
    public void Dispose()
    {
        System.Console.SetIn(defaultInput);
        System.Console.SetOut(defaultOutput);

        output.Dispose();
        input.Dispose();
    }
}
