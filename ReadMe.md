# Simple, Yet Effective ASCII Conversion

## Dependencies

Needs:

- .Net v6.0
- pythonnet v3.0.3
- Python v3.12.0
- Dotnet installed locally

## Local Paths

There are a lot of paths used in this project that need to be configured to your system

Set `output_path` to your project location

```csharp
static string output_path = @"this refers to the output of a text file";
```

Set `conversion_path` to the `conversion` folder in your project

```csharp
static string conversion_path = @"This refers the the location of the conversion script which is a .py file";
```

Set `python_path` to the location of your Python installation and make sure it is the .dll file: e.g. `python312.dll`

```csharp
static string python_path = @"This refers to the installation of Python";
```

## Get Started

Simply have a terminal in Visual Studio Code open and `dotnet restore` to install the `obj` folder and when it's done `dotnet run` to get everything setup
