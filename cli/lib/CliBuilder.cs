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
                return new CommandRenameProxy(command, nameOverride);
            });
        }
    }

    public void AddAsRootCommand<TCommand>(string? descriptionOverwrite = null) where TCommand : Command
    {
        Services.AddScoped<TCommand>();
        Services.AddScoped<RootCommand>(sp =>
        {
            var command = sp.GetRequiredService<TCommand>();
            var description = descriptionOverwrite ?? command.Description ?? throw new InvalidOperationException($"Command {typeof(TCommand).Name} has no description and no description override was provided.");
            
            return new CommandRootingProxy(command, description);
        });
    }

    public CliBuilder()
    {
        Services.AddLogging(l =>
        {
            l.SetMinimumLevel(LogLevel.Trace);
        });
        
        Logging = new LoggingBuilder(Services);
    }

    /// <summary>
    /// Builds the CLI application with the registered commands and services. 
    /// If no root command is pre-registered, a new root command will be created using the provided description.
    /// </summary>
    /// <param name="rootCommandDescription">Description of the root command. Required and used if no root command is pre-registered.</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public Cli Build(string? rootCommandDescription = null)
    {
        var services = Services.BuildServiceProvider();
        var scope = services.CreateScope();
        var preregisteredRootCommand = scope.ServiceProvider.GetService<RootCommand>();
        if (preregisteredRootCommand != null)
        {
            return new Cli(preregisteredRootCommand, scope);
        }

        var allCommands = scope.ServiceProvider.GetServices<Command>();

        var rootCommand = new RootCommand(rootCommandDescription ?? throw new InvalidOperationException("Providing a root command description is required, where no command is registered as root."));
        foreach (var command in allCommands) rootCommand.Subcommands.Add(command);

        return new Cli(rootCommand, scope);
    }

    class LoggingBuilder(IServiceCollection services) : ILoggingBuilder
    {
        public IServiceCollection Services { get; } = services;
    }
}

internal sealed class CommandRenameProxy : Command
{
    public CommandRenameProxy(Command wrapped, string overrideName) : base(overrideName, wrapped.Description)
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

internal sealed class CommandRootingProxy : RootCommand
{
    public CommandRootingProxy(Command wrapped, string description) : base(description)
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