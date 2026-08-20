using Microsoft.Extensions.Logging;

namespace Playground;

public class RunCommand : Command
{
    private readonly Argument<string> commandKeyArgument = new("command")
    {
        Description = "The command to execute.",
        Arity = ArgumentArity.ExactlyOne
    };

    private readonly Dictionary<string, string> commandKeyMap = new()
    {
        { "echo", @"echo ""Hello, World!"" || true && echo ""Second line""" },
        { "replace", @"replace --all-cases SomethingElse SomethingElse" }
    };
    
    private readonly ILogger<RunCommand> logger;

    public RunCommand(ILogger<RunCommand> logger) : base("run", "Greet a person by name.")
    {
        Add(commandKeyArgument);
        SetAction(Execute);

        this.logger = logger;
    }

    private async Task Execute(ParseResult parseResult)
    {
        var key = parseResult.GetValue(commandKeyArgument) ?? "echo";
        if (!commandKeyMap.TryGetValue(key, out var command))
        {
            logger.LogWarning("Unknown command key: {Command}. Defaulting to 'echo'.", key);
            command = commandKeyMap["echo"];
        }

        await Shell.Sh
            .Proxy(command)
            .WithConsoleForwarding()
            .ExecuteAsync();
    }
}