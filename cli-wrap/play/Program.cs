var builder = new CliBuilder();

builder.Logging.AddNiceShell();

builder.AddCommand<RunCommand>();

using var app = builder.Build("A niceshell.cliwrap CLI application.");

return app.Run(args);