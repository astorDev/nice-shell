//global using Console = NiceShell.Console;

var builder = new CliBuilder();

builder.AddCommand<TemplateCommand>();

using var app = builder.Build("A template CLI application.");

return app.Run(args);