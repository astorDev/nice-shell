using CliWrap;

namespace NiceShell;

public static class ConsoleForwarder
{
    public static Command WithConsoleForwarding(this Command command)
    {
        return command.WithStandardOutputPipe(PipeTarget.ToStream(Console.OpenStandardOutput()))
            .WithStandardErrorPipe(PipeTarget.ToStream(Console.OpenStandardError()));
    }
}