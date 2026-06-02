using System.CommandLine;

var rootCommand = new RootCommand("A simple greeting application.")
{
    new GreetCommand()
};

ParseResult parseResult = rootCommand.Parse(args);
return parseResult.Invoke();

public class GreetCommand : Command
{
    private readonly Option<string> _nameOption = new("--name")
    {
        Description = "The name of the person to greet.",
        Required = true
    };

    private readonly Argument<string> pathArgument = new("path")
    {
        Description = "The path from which to read files.",
        Arity = ArgumentArity.ExactlyOne
    };

    public GreetCommand() : base("greet", "Greet a person by name.")
    {
        Add(pathArgument);
        Add(_nameOption);
        SetAction(Execute);
    }

    private void Execute(ParseResult parseResult)
    {
        var name = parseResult.GetRequiredValue(_nameOption);
        var path = parseResult.GetRequiredValue(pathArgument);

        Console.WriteLine($"Hello, {name}!");
        Console.WriteLine($"ls from the path you've provided:");
        Directory.GetFileSystemEntries(path)
            .Select(Path.GetFileName)
            .ToList()
            .ForEach(Console.WriteLine);
    }
}