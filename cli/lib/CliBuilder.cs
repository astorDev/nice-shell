using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NiceShell;

public class CliBuilder
{
    public IServiceCollection Services { get; } = new ServiceCollection();

    public ILoggingBuilder Logging { get; }

    public void AddCommand<TCommand>() where TCommand : Command
    {
        Services.AddScoped<Command, TCommand>();
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
