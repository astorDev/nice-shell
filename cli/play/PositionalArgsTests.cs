namespace Playground;

[TestClass]
public class PositionalArgsTests
{
    private static readonly Argument<string> arg1 = new("arg1")
    {
        Description = "Argument 1 at position 1",
        Arity = ArgumentArity.ZeroOrOne
    };

    private static readonly Argument<string> arg2 = new("arg2")
    {
        Description = "Argument 2 at position 2",
        Arity = ArgumentArity.ZeroOrOne
    };

    private static RootCommand CreateRootCommand()
    {
        var subCommand = new Command("sub", "Sub command description");
        subCommand.SetAction((pr) => Console.WriteLine("Sub command executed"));

        var rootCommand = new RootCommand("Root command description")
        {
            arg1,
            arg2,
            subCommand
        };

        return rootCommand;
    }

    [TestMethod]
    public void SubAfterOneTwo()
    {
        var rootCommand = CreateRootCommand();
        rootCommand.Parse("one two sub").Invoke();
    }

    [TestMethod]
    public void SubAlone()
    {
        var rootCommand = CreateRootCommand();
        rootCommand.Parse("sub").Invoke();
    }

    [TestMethod]
    public void SubAfterOne()
    {
        var rootCommand = CreateRootCommand();
        rootCommand.Parse("one sub").Invoke();
    }

    [TestMethod]
    public void Help()
    {
        var rootCommand = CreateRootCommand();
        rootCommand.Parse("--help").Invoke();
    }

    [TestMethod]
    public void HelpAfterSub()
    {
        var rootCommand = CreateRootCommand();
        rootCommand.Parse("one two sub --help").Invoke();
    }
}