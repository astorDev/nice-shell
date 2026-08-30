using Microsoft.Extensions.Logging;

namespace Playground;

public class ProxyViaProcess : Command
{    
    private readonly Argument<string> commandArgument = new("commandText");

    private readonly ILogger<ProxyViaCliWrapCommand> logger;

    public ProxyViaProcess(ILogger<ProxyViaCliWrapCommand> logger) : base("proxy", "Proxies a command via Process")
    {
        this.Add(commandArgument);
        SetAction(Execute);

        this.logger = logger;
    }

    private async Task Execute(ParseResult parseResult)
    {
        var command = parseResult.GetRequiredValue(commandArgument);

        logger.LogInformation("Proxying `{Command}`", command);

        using var process = await Shell.Sh
            .ProxyProcessStartInfo(command)
            .Run();

        logger.LogInformation("Command `{Command}` exited with code {ReturnCode}", command, process.ExitCode);
    }
}