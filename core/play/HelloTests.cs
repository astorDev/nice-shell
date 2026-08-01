namespace NiceShellCore.Playground;

[TestClass]
public class HelloTests
{
    [TestMethod]
    public void Message()
    {
        var hello = "Hello, Tests!";

        NiceShell.Console.WriteLogLine(hello);
        NiceShell.Console.WriteOutputLine(hello);

        hello.ShouldBe("Hello, Tests!");
    }
}