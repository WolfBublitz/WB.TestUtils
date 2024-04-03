using System;
using System.Collections.Concurrent;
using System.IO;

namespace WB.TestUtils.Console;

internal sealed class BlockingTextReader : TextReader
{
    // ┌────────────────────────────────────────────────────────────────────────────────┐
    // │ Private Fields                                                                 │
    // └────────────────────────────────────────────────────────────────────────────────┘

    private readonly BlockingCollection<string> lines = [];

    // ┌────────────────────────────────────────────────────────────────────────────────┐
    // │ Public Constructors                                                            │
    // └────────────────────────────────────────────────────────────────────────────────┘
    public BlockingTextReader(params string[] lines)
    {
        ArgumentNullException.ThrowIfNull(lines);

        foreach (string line in lines)
        {
            this.lines.Add(line);
        }
    }

    // ┌────────────────────────────────────────────────────────────────────────────────┐
    // │ Public Methods                                                                 │
    // └────────────────────────────────────────────────────────────────────────────────┘
    public void AddLine(string line) => lines.Add(line);

    public override string ReadLine() => lines.Take();

    // ┌────────────────────────────────────────────────────────────────────────────────┐
    // │ Protected Methods                                                              │
    // └────────────────────────────────────────────────────────────────────────────────┘
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (disposing)
        {
            lines.Dispose();
        }
    }
}
