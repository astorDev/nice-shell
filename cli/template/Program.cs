var builder = new CliBuilder();

builder.Logging.AddNiceShell();

builder.AddAsRootCommand<ExampleCommand>();

using var app = builder.Build("A template CLI application.");

return app.Run(args);