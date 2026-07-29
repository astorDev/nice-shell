namespace NiceShellCore.Playground;

[TestClass]
public class HelloTests
{
    [TestMethod]
    public void Message()
    {
        var hello = "Hello, Tests!";

        NiceShell.WriteLogLine(hello);
        NiceShell.WriteOutputLine(hello);

        hello.ShouldBe("Hello, Tests!");
    }
}