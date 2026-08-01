using NiceShell;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddNiceShell(o =>
{
    // o.WriteImmediately = false;
    // o.TimestampFormat = "yyyy-MM-dd HH:mm:ss.fff";
    // o.IncludeScopes = true;
    // o.UseUtcTimestamp = true;
    // o.IncludeCategory = true;
    // o.IncludeLogLevel = true;
});

builder.Logging.SetMinimumLevel(LogLevel.Debug);

var app = builder.Build();

app.Logger.LogDebug("Preparing to output hello in the console!");

Console.WriteLine("Hello!");

app.Logger.LogInformation("Just wrote hello to the console!");

app.Logger.LogDebug("Counting to 10");

for (int i = 1; i <= 10; i++)
{
    Console.WriteLine(i);
}

app.Logger.LogInformation("Finished counting to 10!");

app.Logger.LogInformation("Hello from information!");

app.MapGet("/", () => new {
    Message = "Hello World!"
});

app.Run();