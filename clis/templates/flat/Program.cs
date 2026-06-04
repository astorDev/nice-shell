global using System.CommandLine;

var builder = new CliApplicationBuilder();

builder.AddCommand<TemplateCommand>();

using var app = builder.Build("A template CLI application.");

return app.Run(args);