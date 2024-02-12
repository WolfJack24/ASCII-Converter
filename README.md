# Simple, Yet Effective ASCII Converter

I will maybe add more stuff to convert.

[ASCII Table Credits](https://gist.github.com/angeloped/eaa4e1d0d5c1f707a7381d23c3cf9c4f)
and can be found in the Json file.

## Local Paths

This project depends on local paths:

Add your project location to `output_path` and add `output.txt` or whatever suites you.

```csharp
static string output_path = @"This is where the txt file gets placed";
```

Add the path to the `conversion` folder in your project to `conversion_path`.

```csharp
static string conversion_path = @"The location for converting the ASCII into whatever";
```

Add you Python .dll file to `python_path`.

```csharp
static string python_path = @"The Python path";
```

## Dependencies

Needs:

- net6.0
- pythonnet v3.0.3
- Python v3.12.0
- DotNet installed locally

## Quick Start

To get started, `dotnet restore` to get the `obj` folder and when done, `dotnet run` to get everything sorted.
