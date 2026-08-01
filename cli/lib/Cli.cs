using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;

namespace NiceShell;

public class Cli(RootCommand rootCommand, IServiceScope scope) : IDisposable
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