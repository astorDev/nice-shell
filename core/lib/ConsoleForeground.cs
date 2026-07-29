public class ConsoleForeground
{
    private readonly static object sync = new();

    private static string GetAnsiCode(ConsoleColor color) => color switch
    {
        ConsoleColor.Cyan => "\x1b[36m",
        ConsoleColor.Red => "\x1b[31m",
        ConsoleColor.Yellow => "\x1b[33m",
        ConsoleColor.Gray => "\x1b[37m",
        _ => "\x1b[37m"
    };

    public static IDisposable Push(ConsoleColor color)
    {
        var previousColor = Console.ForegroundColor;
        Console.Error.Write(GetAnsiCode(color));
        return new Scope(previousColor);
    }

    public static void Locking(ConsoleColor color, Action action)
    {
        lock (sync)
        {
            using (Push(color))
            {
                action();
            }
        }
    }

    public static void LockingColorless(Action action)
    {
        lock (sync)
        {
            action();
        }
    }

    public class Scope(ConsoleColor previousColor) : IDisposable
    {
        public void Dispose()
        {
            Console.Error.Write(GetAnsiCode(previousColor));
            Console.ForegroundColor = previousColor;
        }
    }
}
