using CliWrap;
using CliWrap.Buffered;

namespace Playground;

[TestClass]
public class SmokeTests
{
    [TestMethod]
    public async Task HelloWorld()
    {
        var rawCommand = @"echo ""Hello, World!""";

        var result = await CliWrap.Cli.Wrap("/bin/bash")
            .WithArguments(["-c", rawCommand])
            .WithStandardOutputPipe(PipeTarget.ToDelegate(Console.WriteLine))
            .ExecuteBufferedAsync();

        Console.WriteLine(result.StandardOutput);

        result.StandardOutput.ShouldBe("Hello, World!" + Environment.NewLine);
    }
}