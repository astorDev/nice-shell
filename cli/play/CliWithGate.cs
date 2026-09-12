namespace Playground;

[TestClass]
public class CliWithGate
{
    [TestMethod]
    public void Happy()
    {
        var args = new[] { "egor", "Hello" };
        var builder = new CliBuilder();

        builder.AddCliGate<ExampleGate>();

        using var cli = builder.Build("cli with gate happy", args);
        cli.Run(args);
    }

    [TestMethod]
    public void Help()
    {
        var args = new[] { "--help" };
        var builder = new CliBuilder();

        builder.AddCliGate<ExampleGate>();

        using var cli = builder.Build("cli with gate help", args);

        cli.Run(args);
    }

    [TestMethod]
    public void NoArgs()
    {
        var args = Array.Empty<string>();
        var builder = new CliBuilder();

        builder.AddCliGate<ExampleGate>();

        using var cli = builder.Build("cli with gate no args", args);
        cli.Run(args);
    }
}

public class ExampleGate : Command, ICliGate
{
    public static readonly Argument<string> NameArgument = new("name");

    public ExampleGate() : base("happy", "A happy command.")
    {
        Add(NameArgument);
    }

    public CliGateResult Process(string[] args)
    {
        var parsed = Parse(args);
        var name = parsed.GetValue(NameArgument);

        if (name == null)
        {
            return CliGateResult.Stop(parsed.Invoke());
        }

        return CliGateResult.ContinueWith([ new SubOne(name) ]);
    }
}

public class SubOne : Command
{
    public static readonly Argument<string> Greeting = new("greeting");

    public SubOne(string name) : base(name, "A sub command.")
    {
        Add(Greeting);
        SetAction(Execute);
    }

    public void Execute(ParseResult parseResult)
    {
        var greeting = parseResult.GetValue(Greeting);

        Console.WriteLine($"{greeting}, {Name}!");
    }
}