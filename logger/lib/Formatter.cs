using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace NiceShell;

public static class NiceShellConsoleLoggerExtensions
{
    public static ILoggingBuilder AddNiceShellConsole(this ILoggingBuilder builder, Action<NiceShellConsoleFormatterSettings>? configure = null)
    {
        builder.AddConsole(c => {
            c.LogToStandardErrorThreshold = LogLevel.None;
            c.FormatterName = NiceShellFormatter.FormatterName;
        });

        builder.AddConsoleFormatter<NiceShellFormatter, NiceShellConsoleFormatterSettings>((o) => {
            configure?.Invoke(o);
        });

        return builder;
    }
}

public class NiceShellConsoleFormatterSettings : ConsoleFormatterOptions
{
    public string WarningPrefix { get; set; } = "⚠️ "; 

    public bool IncludeCategory { get; set; } = false;

    public bool IncludeLogLevel { get; set; } = false;

    public NiceShellConsoleFormatterSettings()
    {
        IncludeScopes = false;
        TimestampFormat = null;
        UseUtcTimestamp = true;
    }
}

public class NiceShellFormatter(IOptions<NiceShellConsoleFormatterSettings> options) : ConsoleFormatter(FormatterName)
{
    public const string FormatterName = "nice-shell";
    const string DefaultForegroundColor = "\x1B[39m\x1B[22m";

    public static string GetColorEspaceCode(LogLevel logLevel) => logLevel switch
    {
        LogLevel.None or LogLevel.Trace or LogLevel.Debug or LogLevel.Information => "\x1B[1m\x1B[36m",
        LogLevel.Warning => "\x1B[1m\x1B[33m",
        LogLevel.Error or LogLevel.Critical => "\x1B[1m\x1B[31m",
        _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null),
    };

    public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
    {
        var colorEscapeCode = GetColorEspaceCode(logEntry.LogLevel);
        textWriter.Write(colorEscapeCode);

        var anyPrefix = false;

        if (options.Value.TimestampFormat is not null)
        {
            var time = options.Value.UseUtcTimestamp ? DateTime.UtcNow : DateTime.Now;
            var timestamp = time.ToString(options.Value.TimestampFormat);
            textWriter.Write($"{timestamp}");
            anyPrefix = true;
        }

        if (options.Value.IncludeScopes)
        {
            scopeProvider?.ForEachScope((scope, writer) =>
            {
                writer.Write($" {scope}");
                anyPrefix = true;
            }, textWriter);
        }

        
        if (options.Value.IncludeCategory)
        {
            textWriter.Write($" {logEntry.Category}");
            anyPrefix = true;
        }
        
        if (options.Value.IncludeLogLevel)
        {
            textWriter.Write($" {logEntry.LogLevel}");
            anyPrefix = true;
        }

        if (anyPrefix)
        {
            textWriter.Write(": ");
        }

        if (logEntry.LogLevel == LogLevel.Warning)
        {
            textWriter.Write(options.Value.WarningPrefix);
        }

        var message = logEntry.Formatter(logEntry.State, logEntry.Exception);
        textWriter.Write(message);
        textWriter.Write(DefaultForegroundColor);

        textWriter.WriteLine();
    }
}
