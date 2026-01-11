using System.Diagnostics;

namespace NiceShell.Playground;

[TestClass]
public class HelloTests
{
    [TestMethod]
    public void Pwd()
    {
        using var process = Process.Start(new ProcessStartInfo
        {
            FileName = "pwd",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        });

        var output = process?.StandardOutput.ReadToEnd();
        process?.WaitForExit();

        Console.WriteLine(output);
    }

    [TestMethod]
    public void DockerVersion()
    {
        using var process = Process.Start(new ProcessStartInfo
        {
            FileName = "docker",
            Arguments = "--version",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        });

        var output = process?.StandardOutput.ReadToEnd();
        process?.WaitForExit();

        Console.WriteLine(output);
    }

    [TestMethod]
    public void DotnetNewList()
    {
        using var process = Process.Start(new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = "new --list",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        });

        var output = process?.StandardOutput.ReadToEnd();
        process?.WaitForExit();

        Console.WriteLine(output);
    }
}

[TestClass]
public class AtomicCommandTests
{
    [TestMethod]
    public void Pwd()
    {
        var output = AtomicCommand.Run("pwd");
        Console.WriteLine(output);
    }

    [TestMethod]
    public void DockerVersion()
    {
        var output = AtomicCommand.Run("docker --version");
        Console.WriteLine(output);
    }

    [TestMethod]
    public void DotnetNewList()
    {
        var output = AtomicCommand.Run("dotnet new --list");
        Console.WriteLine(output);
    }

    [TestMethod]
    public void Ollama()
    {
        var output = AtomicCommand.Run("ollama");
        Console.WriteLine(output);
    }

    [TestMethod]
    public void HelloGemma()
    {
        var output = AtomicCommand.Run("""
        ollama run gemma3:4b "Hello, Gemma"
        """);
        Console.WriteLine(output);
    }

    [TestMethod]
    public void Grammar()
    {
        var output = AtomicCommand.Run("""
        ollama run gemma3:4b "
        Check grammar of the following German text:

        ```text
        Was heißt du?
        ```
        """);

        Console.WriteLine(output);
    }
}