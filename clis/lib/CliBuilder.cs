using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;

namespace NiceShell;

public class CliBuilder
{
    public IServiceCollection Services { get; } = new ServiceCollection();

    public void AddCommand<TCommand>() where TCommand : Command
    {
        Services.AddScoped<Command, TCommand>();
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
}
