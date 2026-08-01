using NiceShell;

var builder = WebApplication.CreateBuilder(args);

// builder.Logging.AddNiceShellConsole(o =>
// {
//     o.TimestampFormat = "yyyy-MM-dd HH:mm:ss.fff";
//     o.IncludeScopes = true;
//     o.UseUtcTimestamp = true;
//     o.IncludeCategory = true;
//     o.IncludeLogLevel = true;
// });

builder.Logging.AddNiceShellConsole();

var app = builder.Build();

app.Logger.LogInformation("Hello from information!");

app.MapGet("/", () => new {
    Message = "Hello World!"
});

app.Run();