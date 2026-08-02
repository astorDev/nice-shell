global using NiceShell;
global using System.CommandLine;

var builder = new CliBuilder();

builder.Logging.AddNiceShell();

builder.AddCommand<ExampleCommand>();
builder.AddCommand<AlternativeCommand>("alt");
//builder.AddCommand<AlternativeCommand>();

using var app = builder.Build("An example CLI application.");

return app.Run(args);