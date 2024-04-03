# WB.TestUtils

A collection of utilities for writing tests in C#.

## Features

### Scrubbers

A set of extension methods for scrubbing dynamic data from strings. Currently the following scrubbers are available:

- `Scrub`: A general purpose scrubber that replaces all occurrences of a given string with a specified replacement.
- `ScrubDateTime`: A scrubber that replaces all occurrences of a date and time with a specified replacement.

## Installation

```bash
dotnet add package WB.TestUtils
```
