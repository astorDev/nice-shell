using System.CommandLine;
using Confi;
using Microsoft.Extensions.Configuration;

public static class LoggingCliConfiguration
{
    /// <summary>
    /// Marker property for collection addition syntax i.e. 
    /// <code>
    /// new Command() 
    /// { 
    ///     LoggingCliConfiguration.Options 
    /// }
    /// </code>
    ///  
    /// Internally calls <code>command.AddLoggingCliOptions()</code>
    /// </summary>
    /// <returns></returns>
    public static readonly LoggingCliConfigurationSymbols Options = new();

    public static readonly Option<bool?> Verbose = new ("--verbose", "-v")
    {
        Description = "Set Minimum Log Level to Trace"
    };

    public static readonly Option<bool?> Quiet = new ("--quiet")
    {
        Description = "Sets Minimum Log Level to Error"
    };

    public static readonly Option<string> LogLevel = CreateLogLevelOption();

    public static Option<string> CreateLogLevelOption()
    {
        var option = new Option<string>("--log-level")
        {
            Description = "Sets Minimum Log Level to the Specified Level",
            HelpName = "None|Trace|Debug|Information|Warning|Error|Critical"
        };

        option.AcceptOnlyFromAmong("None", "Trace", "Debug", "Information", "Warning", "Error", "Critical");
        return option;
    }

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

public record LoggingCliConfigurationSymbols();

public static class CommandExtensions
{
    public static void Add(this Command command, LoggingCliConfigurationSymbols loggingCliConfiguration)
    {
        command.AddLoggingCliOptions();
    }
}