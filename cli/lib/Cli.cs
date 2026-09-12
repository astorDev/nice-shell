using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NiceShell;

public partial class Cli(RootCommand rootCommand, IServiceScope scope, ILogger<Cli> logger) : IDisposable
{
    public IServiceProvider Services { get; } = scope.ServiceProvider;

    public void Dispose()
    {
        scope.Dispose();
        GC.SuppressFinalize(this);
    }

    public int Run(Func<int> action)
    {
        try
        {
            return action();
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "An error occurred while running the CLI.");
            return 1;
        }
    }

    public int Run(Action action)
    {
        try
        {
            action();
            return 0;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "An error occurred while running the CLI.");
            return 1;
        }
    }

    public async Task<int> RunAsync(Func<Task> action)
    {
        try
        {
            await action();
            return 0;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "An error occurred while running the CLI.");
            return 1;
        }
    }

    public async Task<int> RunAsync(Func<Task<int>> action)
    {
        try
        {
            return await action();
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "An error occurred while running the CLI.");
            return 1;
        }
    }

    public int Run(string[] args) => Run(() =>
    {
        var parseResult = rootCommand.Parse(args);
        return parseResult.Invoke();
    });

    public Task<int> RunAsync(string[] args) => RunAsync(async () =>
    {
        var parseResult = rootCommand.Parse(args);
        return await parseResult.InvokeAsync();
    });
}