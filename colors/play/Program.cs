using NiceShell;
using Nist;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSimpleConsole(c => {
    c.SingleLine = true;
    c.ColorBehavior = Microsoft.Extensions.Logging.Console.LoggerColorBehavior.Disabled;
});

var app = builder.Build();

app.UseHttpIOLogging();

app.MapGet("/", () => new {
    Message = "Hello World!"
});

Console.Write(SelectGraphicRendition.Dim);

app.Run();

Console.Write(SelectGraphicRendition.NormalIntensity);