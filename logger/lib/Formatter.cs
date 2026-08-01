using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace NiceShell;

public static class NiceShellConsoleLoggerExtensions
{
    public static ILoggingBuilder AddNiceShell(this ILoggingBuilder builder, Action<NiceShellConsoleFormatterSettings>? configure = null)
    {
        var settings = new NiceShellConsoleFormatterSettings();
        configure?.Invoke(settings);

        if (settings.WriteImmediately)
        {
            builder.Services.Configure<NiceShellConsoleFormatterSettings>(o => configure?.Invoke(o));
            builder.Services.AddSingleton<ILoggerProvider, NiceShellImmediateLoggerProvider>();
            return builder;
        }

        builder.AddConsole(c => {
            c.LogToStandardErrorThreshold = LogLevel.Trace;
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

    /// <summary>
    /// By-passes ConsoleLogger's queuing technique and writes to console immediately.
    /// Needed for cases like CLI where logs need to be in-sync with normal output.
    /// Default is true.
    /// </summary>
    public bool WriteImmediately { get; set; } = true;

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

    public static string GetColorEscapeCode(LogLevel logLevel) => logLevel switch
    {
        LogLevel.None or LogLevel.Trace or LogLevel.Debug or LogLevel.Information => "\x1B[1m\x1B[36m",
        LogLevel.Warning => "\x1B[1m\x1B[33m",
        LogLevel.Error or LogLevel.Critical => "\x1B[1m\x1B[31m",
        _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null),
    };

    public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
    {
        var colorEscapeCode = GetColorEscapeCode(logEntry.LogLevel);
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

        if (logEntry.Exception is not null)
        {
            textWriter.WriteLine();
            textWriter.Write(logEntry.Exception);
        }

        textWriter.Write(DefaultForegroundColor);
        textWriter.WriteLine();
}

/// <summary>
/// By-passes ConsoleLogger's queuing technique and writes to console immediately.
/// Needed for cases like CLI where logs need to be in-sync with normal output.
/// </summary>
public class NiceShellImmediateLoggerProvider(IOptions<NiceShellConsoleFormatterSettings> options) : ILoggerProvider
{
    // Reuses NiceShellFormatter's core writer logic instead of duplicating it.
    private readonly NiceShellFormatter formatter = new(options);
    private readonly IExternalScopeProvider scopeProvider = new LoggerExternalScopeProvider();

    public ILogger CreateLogger(string categoryName) => new ImmediateLogger(categoryName, formatter, scopeProvider);

    public void Dispose() { }

    class ImmediateLogger(string category, NiceShellFormatter formatter, IExternalScopeProvider scopeProvider) : ILogger
    {
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => scopeProvider.Push(state);

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatterFn)
        {
            var entry = new LogEntry<TState>(logLevel, category, eventId, state, exception, formatterFn);

            formatter.Write(in entry, scopeProvider, Console.Error);
            Console.Error.Flush();
        }
    }
}
