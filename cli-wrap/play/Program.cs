var builder = new CliBuilder();

builder.Logging.AddNiceShell();

builder.AddCommand<RunCommand>();

using var app = builder.Build("A niceshell.cliwrap CLI application.");

var rawCommand = @"echo ""Hello, World!"" || true && echo ""Second line""";

await Shell.Bash
    .Proxy(rawCommand)
    .WithConsoleForwarding()
    .ExecuteAsync();

// return app.Run(args);