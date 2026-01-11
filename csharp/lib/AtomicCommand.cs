using System.Diagnostics;

namespace NiceShell;

public class AtomicCommand
{
    public static string Run(string commmand)
    {
        var parts = commmand.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        var startInfo = new ProcessStartInfo
        {
            FileName = parts[0],
            Arguments = parts.Length > 1 ? parts[1] : string.Empty,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        using var process = Process.Start(startInfo) ?? throw new InvalidOperationException("Failed to start process.");
        var output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        return output;
    }
}