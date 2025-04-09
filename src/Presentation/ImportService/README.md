# Import Service (CLI)

This directory contains a command-line interface application that provides data import functionality.

## What goes here?

- **Program Entry Point**: Main program class and execution logic
- **Command Line Options**: Classes defining CLI options and arguments
- **Commands**: Implementation of various CLI commands
- **Console Output Formatting**: Utilities for formatting console output

## Example Structure

```
ImportService/
├── Commands/
│   ├── ImportCustomersCommand.cs
│   └── ImportProductsCommand.cs
├── Options/
│   ├── ImportOptions.cs
│   └── BaseOptions.cs
├── Formatters/
│   └── ConsoleFormatter.cs
├── Program.cs
└── appsettings.json
```

This CLI application follows the same Clean Architecture principles as the rest of the solution, depending on the Application and Infrastructure layers and not containing business logic directly.
