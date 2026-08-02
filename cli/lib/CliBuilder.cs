using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NiceShell;

public class CliBuilder
{
    public IServiceCollection Services { get; } = new ServiceCollection();

    public ILoggingBuilder Logging { get; }

    public void AddCommand<TCommand>(string? nameOverride = null) where TCommand : Command
    {
        Services.AddScoped<TCommand>();
        
        if (nameOverride == null)
        {
            Services.AddScoped<Command, TCommand>();
        }
        else
        {
            Services.AddScoped<Command>(sp => 
            {
                var command = sp.GetRequiredService<TCommand>();
                return new CommandProxy(command, nameOverride);
            });
        }
    }

    public CliBuilder()
    {
        Services.AddLogging(l =>
        {
            l.SetMinimumLevel(LogLevel.Trace);
        });
        
        Logging = new LoggingBuilder(Services);
    }

    public Cli Build(string rootCommandDescription)
    {
        var services = Services.BuildServiceProvider();
        var scope = services.CreateScope();
        var allCommands = scope.ServiceProvider.GetServices<Command>();

        var rootCommand = new RootCommand(rootCommandDescription);
        foreach (var command in allCommands) rootCommand.Subcommands.Add(command);

        return new Cli(rootCommand, scope);
    }

    class LoggingBuilder(IServiceCollection services) : ILoggingBuilder
    {
        public IServiceCollection Services { get; } = services;
    }
}

internal sealed class CommandProxy : Command
{
    public CommandProxy(Command wrapped, string overrideName) : base(overrideName, wrapped.Description)
    {
        foreach (var subcommand in wrapped.Subcommands) Subcommands.Add(subcommand);
        foreach (var option in wrapped.Options) Options.Add(option);
        foreach (var argument in wrapped.Arguments) Arguments.Add(argument);
        foreach (var alias in wrapped.Aliases) Aliases.Add(alias);

        Action = wrapped.Action;
        Hidden = wrapped.Hidden;
        TreatUnmatchedTokensAsErrors = wrapped.TreatUnmatchedTokensAsErrors;
    }
}