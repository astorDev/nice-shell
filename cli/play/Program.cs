global using NiceShell;
global using System.CommandLine;

var builder = new CliBuilder();

builder.Logging.AddNiceShell();

builder.AddCommand<ExampleCommand>();
builder.AddCommand<AlternativeCommand>("alt");

// To check usage of the AlternativeCommand with it's original name:
// builder.AddCommand<AlternativeCommand>();

// Commands Upgraded to root scenario:
// builder.AddAsRootCommand<ExampleCommand>("This is an example command that serves as the root command.");
// builder.AddAsRootCommand<AlternativeCommand>("This is an alternative command that serves as the root command.");

using var app = builder.Build("An example CLI application.");

return app.Run(args);