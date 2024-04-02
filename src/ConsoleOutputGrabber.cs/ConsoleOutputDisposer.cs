// ----------------------------------------------------------------------------
// <copyright file="ConsoleOutputDisposer.cs" company="WB">
// Copyright (c) WB Project
// </copyright>
// ----------------------------------------------------------------------------

using System;
using System.IO;

namespace WB.TestUtils.Console;

/// <summary>
/// Disposes the console output grabbing.
/// </summary>
/// <remarks>
/// This class is returned by the <see cref="ConsoleOutputGrabber.Start"/> method
/// and should be disposed to stop grabbing the console output.
/// </remarks>
/// <seealso cref="IDisposable"/>
[Obsolete("Use TestConsole instead.")]
internal sealed class ConsoleOutputDisposer : IDisposable
{
    // ┌────────────────────────────────────────────────────────────────┐
    // │ PRIVATE Fields                                                 │
    // └────────────────────────────────────────────────────────────────┘
    private readonly TextWriter defaultOutput;

    private readonly StringWriter consoleOutput = new();

    private readonly ConsoleOutputGrabber consoleOutputGrabber;

    // ┌────────────────────────────────────────────────────────────────┐
    // │ PUBLIC Constructors                                            │
    // └────────────────────────────────────────────────────────────────┘

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleOutputDisposer"/> class.
    /// </summary>
    /// <param name="consoleOutputGrabber">The <see cref="ConsoleOutputGrabber"/>.</param>
    public ConsoleOutputDisposer(ConsoleOutputGrabber consoleOutputGrabber)
    {
        this.consoleOutputGrabber = consoleOutputGrabber;
        defaultOutput = System.Console.Out;
        System.Console.SetOut(consoleOutput);
    }

    // ┌────────────────────────────────────────────────────────────────┐
    // │ PUBLIC Methods                                                 │
    // └────────────────────────────────────────────────────────────────┘

    /// <inheritdoc/>
    public void Dispose()
    {
        consoleOutputGrabber.Output = consoleOutput.ToString();

        System.Console.SetOut(defaultOutput);
        consoleOutput.Dispose();
    }
}
