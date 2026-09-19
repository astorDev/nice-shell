using System.CommandLine;
using Confi;
using Microsoft.Extensions.Configuration;

public static class LoggingCliConfiguration
{
    public static readonly Option<bool?> Verbose = new ("--verbose", "-v");
    public static readonly Option<bool?> Quiet = new ("--quiet");
    public static readonly Option<string> LogLevel = new ("--log-level");

    public static readonly ICliOptionConfigurator VerboseConfigurator = Verbose.Configuring("Logging:LogLevel:Default", "Trace", "Information");
    public static readonly ICliOptionConfigurator QuietConfigurator = Quiet.Configuring("Logging:LogLevel:Default", "Error", "Information");
    public static readonly ICliOptionConfigurator LogLevelConfigurator = LogLevel.Configuring("Logging:LogLevel:Default");

    public static void AddLoggingCliOptions(this IConfigurationBuilder builder, string[] args) => builder.AddCliOptions(args,
        QuietConfigurator,
        VerboseConfigurator,
        LogLevelConfigurator
    );

    public static void AddLoggingCliOptions(this Command command)
    {
        command.Add(Verbose);
        command.Add(Quiet);
        command.Add(LogLevel);
    }
}