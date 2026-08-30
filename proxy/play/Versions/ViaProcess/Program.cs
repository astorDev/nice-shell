var builder = new CliBuilder();

builder.Logging.AddNiceShell();

builder.AddAsRootCommand<ProxyViaProcess>();

using var app = builder.Build("A niceshell.proxy CLI application.");

return app.Run(args);