global using NiceShell;
global using System.CommandLine;

var builder = new CliBuilder();

builder.Logging.AddNiceShellConsole();

builder.AddCommand<ExampleCommand>();

using var app = builder.Build("An example CLI application.");

return app.Run(args);