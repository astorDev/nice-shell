using Microsoft.Extensions.Logging;
using CliWrap;

namespace Playground;

public class ProxyViaCliWrapCommand : System.CommandLine.Command
{    
    private readonly Argument<string> commandArgument = new("commandText");

    private readonly ILogger<ProxyViaCliWrapCommand> logger;

    public ProxyViaCliWrapCommand(ILogger<ProxyViaCliWrapCommand> logger) : base("proxy", "Proxies a command via CliWrap")
    {
        this.Add(commandArgument);
        SetAction(Execute);

        this.logger = logger;
    }

    private async Task Execute(ParseResult parseResult)
    {
        var command = parseResult.GetRequiredValue(commandArgument);

        logger.LogInformation("Proxying `{Command}`", command);

        await CliWrap.Cli.Wrap(Shell.Sh.File)
            .WithArguments([ Shell.Sh.Flags[0], command ])
            .WithStandardErrorPipe(PipeTarget.ToStream(Console.OpenStandardError()))
            .WithStandardOutputPipe(PipeTarget.ToStream(Console.OpenStandardOutput()))
            .WithStandardInputPipe(PipeSource.FromStream(Console.OpenStandardInput()))
            .ExecuteAsync();
    }
}