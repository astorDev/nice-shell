var builder = new CliBuilder();

builder.Logging.AddNiceShell();

builder.AddAsRootCommand<CommandNameCommand>();

using var app = builder.Build("A template CLI application.");

return app.Run(args);