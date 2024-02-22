# Simple, Yet Effective ASCII Converter

I will maybe add more stuff to convert.

[ASCII Table Credits](https://gist.github.com/angeloped/eaa4e1d0d5c1f707a7381d23c3cf9c4f)
and can be found in the Json file.

## Local Paths

### This project depends on local paths:

Your project location gets set automatically but you can edit the `FileName` to `output.txt`
or whatever suites you.

```csharp
    static readonly string FileName = "output.txt";
    static readonly string OutputPath = Path.Combine(Environment.CurrentDirectory, FileName);
```

The conversion filepath gets set automatically now, no more manual updating.

```csharp
    static readonly string ConversionPath = Path.Combine(Environment.CurrentDirectory, "conversion\\");
```

Your Python .dll is more private now by using a `.env` file.
This is what your .env file should look like:

```properties
PYTHON_PATH="Python .dll Path"
```

It is then loaded in the `Main` function:

```csharp
    var dotenv = Path.Combine(Environment.CurrentDirectory, ".env");
    DotEnv.Load(dotenv);
```

And finally added to the global variable `PythonPath`:

```csharp
    static readonly string PythonPath = Environment.GetEnvironmentVariable("PYTHON_PATH");
```

## Dependencies

Needs:

- .net6.0
- pythonnet v3.0.3
- Python v3.12.0
- DotNet installed locally

## Quick Start

To get started, `dotnet restore` to get the build directory's
and when done, `dotnet run` to get everything sorted.
