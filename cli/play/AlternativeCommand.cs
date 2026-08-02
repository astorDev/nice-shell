public class AlternativeCommand : Command
{
    private readonly ILogger<AlternativeCommand> logger;

    private readonly Option<string> messageOption = new("--message")
    {
        Description = "The message to display.",
        Required = true
    };

    public AlternativeCommand(ILogger<AlternativeCommand> logger) : base("alternative", "Display an alternative message.")
    {
        Add(messageOption);
        SetAction(Execute);
        this.logger = logger;
    }

    private void Execute(ParseResult parseResult)
    {
        var message = parseResult.GetRequiredValue(messageOption);

        logger.LogTrace("Displaying message {Message}...", message);

        Console.WriteLine($"Message: {message}");

        logger.LogInformation("Displayed message successfully.");
    }
}
