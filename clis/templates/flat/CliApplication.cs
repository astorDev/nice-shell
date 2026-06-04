using Microsoft.Extensions.DependencyInjection;

public class CliApplicationBuilder
{
    public IServiceCollection Services { get; } = new ServiceCollection();

    public void AddCommand<TCommand>() where TCommand : Command
    {
        Services.AddScoped<Command, TCommand>();
    }

    public CliApplication Build(string rootCommandDescription)
    {
        var services = Services.BuildServiceProvider();
        var scope = services.CreateScope();
        var allCommands = scope.ServiceProvider.GetServices<Command>();

        var rootCommand = new RootCommand(rootCommandDescription);
        foreach (var command in allCommands) rootCommand.Subcommands.Add(command);

        return new CliApplication(rootCommand, scope);
    }
}

public class CliApplication(RootCommand rootCommand, IServiceScope scope) : IDisposable
{
    public int Run(string[] args)
    {
        ParseResult parseResult = rootCommand.Parse(args);
        return parseResult.Invoke();
    }

    public void Dispose()
    {
        scope.Dispose();
        GC.SuppressFinalize(this);
    }
}