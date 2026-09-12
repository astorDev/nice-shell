global using System.CommandLine;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;

namespace NiceShell;

public class Cli(IServiceScope scope, ILogger<Cli> logger, BuildResult buildResult) : IDisposable
{
    public IServiceProvider Services { get; } = scope.ServiceProvider;

    public void Dispose()
    {
        scope.Dispose();
        GC.SuppressFinalize(this);
    }

    public int Run(string[] args)
    {
        return buildResult.Execute(args, logger);
    }
}