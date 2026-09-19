using NiceShell;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddLoggingCliOptions(args);

builder.Logging.ClearProviders();
builder.Logging.AddNiceShell(o =>
{
    // o.WriteImmediately = false;
    // o.TimestampFormat = "yyyy-MM-dd HH:mm:ss.fff";
    o.IncludeLogLevel = true;
    // o.UseUtcTimestamp = true;
    // o.IncludeCategory = true;
    // o.IncludeLogLevel = true;
});

var app = builder.Build();

var logger = app.Logger;

Console.WriteLine("Logging:LogLevel:Default = " + builder.Configuration["Logging:LogLevel:Default"]);

logger.LogDebug("Preparing to output hello in the console!");

Console.WriteLine("Hello!");

logger.LogInformation("Just wrote hello to the console!");

logger.LogTrace("Counting to 3");
logger.LogWarning("Don't repeat this at home!");

for (int i = 1; i <= 3; i++)
{
    Console.WriteLine(i);
}

logger.LogDebug("Finished counting to 3!");
logger.LogInformation("Hello from information!");

try
{
    throw new Exception("This is a sample exception");
}
catch (Exception exception)
{
    logger.LogError(exception, "Let's say this was an error, but we've continued anyway!");
    logger.LogCritical(exception, "This is a critical error!");
}