public class ExampleCommand : Command
{
    private readonly ILogger<ExampleCommand> logger;

    private readonly Option<string> nameOption = new("--name")
    {
        Description = "The name of the person to greet.",
        Required = true
    };

    private readonly Argument<string> pathArgument = new("path")
    {
        Description = "The path from which to read files.",
        Arity = ArgumentArity.ExactlyOne
    };

    public ExampleCommand(ILogger<ExampleCommand> logger) : base("example", "Greet a person by name.")
    {
        Add(pathArgument);
        Add(nameOption);
        SetAction(Execute);
        this.logger = logger;
    }

    private void Execute(ParseResult parseResult)
    {
        var name = parseResult.GetRequiredValue(nameOption);
        var path = parseResult.GetRequiredValue(pathArgument);

        logger.LogTrace("Greeting {Name}...", name);
        System.Console.Error.Flush();

        Console.WriteLine($"Hello, {name}!");

        logger.LogInformation("Greeted {Name} successfully.", name);

        logger.LogInformation("Getting files from the path: {Path}", path);

        Console.WriteLine($"ls from the path you've provided:");
        Directory.GetFileSystemEntries(path)
            .Select(Path.GetFileName)
            .ToList()
            .ForEach(Console.WriteLine);
    }
}