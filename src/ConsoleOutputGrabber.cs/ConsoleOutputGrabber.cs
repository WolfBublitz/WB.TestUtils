// ----------------------------------------------------------------------------
// <copyright file="ConsoleOutputGrabber.cs" company="WB">
// Copyright (c) WB Project
// </copyright>
// ----------------------------------------------------------------------------

using System;

namespace WB.TestUtils.Console;

/// <summary>
/// Grabs the output of the console and provides it as a <see cref="Output"/> property.
/// </summary>
/// <example>
/// The following example demonstrates how to use the <see cref="ConsoleOutputGrabber"/> class to grab the
/// console output.
/// <code>
/// using System;
/// using WB.TestUtils.Console;
/// ConsoleOutputGrabber grabber = new();
///
/// using (IDisposable disposer = grabber.Start())
/// {
///    Console.WriteLine("Hello, World!");
/// }
///
/// Console.WriteLine(grabber.Output);
/// </code>
/// </example>
public sealed class ConsoleOutputGrabber
{
    /// <summary>
    /// Gets the grabbed console output.
    /// </summary>
    public string? Output { get; internal set; }

    /// <summary>
    /// Starts grabbing the console output and returns a disposable object to stop grabbing.
    /// </summary>
    /// <returns>An <see cref="IDisposable"/> that stops grabbing on dispose.</returns>
    public IDisposable Start() => new ConsoleOutputDisposer(this);
}
