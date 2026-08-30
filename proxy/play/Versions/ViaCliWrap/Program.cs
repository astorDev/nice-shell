var builder = new CliBuilder();

builder.Logging.AddNiceShell();

builder.AddAsRootCommand<ProxyViaCliWrapCommand>();

using var app = builder.Build("A niceshell.proxy CLI application.");

return app.Run(args);