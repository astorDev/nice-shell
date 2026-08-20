using CliWrap;

namespace NiceShell;

public static class ConsoleForwarder
{
    public static Command WithConsoleForwarding(this Command command)
    {
        return command.WithStandardOutputPipe(PipeTarget.ToStream(Console.OpenStandardOutput()))
            .WithStandardErrorPipe(PipeTarget.ToStream(Console.OpenStandardError()));
    }

    public static Command WithDimmedConsoleForwarding(this Command command)
    {
        return command.WithStandardOutputPipe(ConsoleForwarding.Dimmed(Console.Out))
            .WithStandardErrorPipe(ConsoleForwarding.Dimmed(Console.Error));
    }
}

public class ConsoleForwarding
{
    public static PipeTarget Dimmed(TextWriter writer) => PipeTarget.ToDelegate(line =>
    {
        writer.Write("\x1b[2m");
        writer.WriteLine(line);
        writer.Write("\x1b[22m");
    });
}