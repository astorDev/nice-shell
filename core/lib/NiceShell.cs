namespace NiceShell;

public class Console
{
    public static void WriteLogLine(string message, ConsoleColor color = ConsoleColor.Cyan) => 
        ConsoleForeground.Locking(color, () => System.Console.Error.WriteLine(message));

    public static void WriteErrorLine(string message) => 
        WriteLogLine(message, ConsoleColor.Red);

    public static void WriteWarningLine(string message, string prefix = "⚠️ ") => 
        WriteLogLine(prefix + message, ConsoleColor.Yellow);

    public static void WriteOutputLine(string message) =>
        ConsoleForeground.LockingColorless(() => System.Console.Out.WriteLine(message));

    public static void WriteOutput(string message) =>
        ConsoleForeground.LockingColorless(() => System.Console.Out.Write(message));

    public static void WriteOutputAndNewLogLine(string message) =>
        ConsoleForeground.LockingColorless(() =>
        {
            System.Console.Out.Write(message);
            System.Console.Error.WriteLine();
        });
}