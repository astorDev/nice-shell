namespace NiceShell;

public static class CliGateRegistrationExtensions
{
    public static IServiceCollection AddCliGate<TGate>(this CliBuilder cliBuilder) where TGate : class, ICliGate
    {
        cliBuilder.Services.AddSingleton<ICliGate, TGate>();
        return cliBuilder.Services;
    }
}

public interface ICliGate
{
    CliGateResult Process(string[] args);
}

public record CliGateResult(int? ExitCode, Command[]? ResolvedCommands)
{
    public static CliGateResult Stop(int exitCode) => new(exitCode, null);
    public static CliGateResult ContinueWith(params Command[] resolvedCommands) => new(null, resolvedCommands);

    public bool IsStopped => ExitCode.HasValue;
}

public class CliGateRootCommandResolutionStrategy(IServiceScope serviceScope, ILogger<Cli> logger, string rootCommandDescription, string[] args)
{
    public Cli? Apply()
    {
        var gate = serviceScope.ServiceProvider.GetService<ICliGate>();
        if (gate == null) return null;

        var result = gate.Process(args);
        if (result.ExitCode.HasValue)
        {
            Environment.Exit(result.ExitCode!.Value);
        }

        var rootCommand = new RootCommand(rootCommandDescription);
        foreach (var command in result.ResolvedCommands!)
        {
            rootCommand.Add(command);
        }

        return new Cli(serviceScope, logger, BuildResult.RootCommandReady(rootCommand));
    }
}