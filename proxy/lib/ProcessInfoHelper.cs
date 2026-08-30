using System.Diagnostics;

namespace NiceShell;

public static class ProcessHelper
{
    public static ProcessStartInfo ProxyProcessStartInfo(this Shell shell, string command)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = shell.File
        };

        foreach (var flag in shell.Flags)
        {
            startInfo.ArgumentList.Add(flag);
        }

        startInfo.ArgumentList.Add(command);

        return startInfo;
    }

    /// <summary>
    /// Starts a process with the specified <see cref="ProcessStartInfo"/> and waits for it to exit asynchronously.
    /// <see cref="useProcess"/> can be provided to perform actions on the process before waiting for it to exit.
    /// Returns the <see cref="Process"/> instance after it has exited. Primarily useful for diagnostic purposes.
    /// </summary>
    public static async Task<Process> Run(this ProcessStartInfo startInfo, Action<Process>? useProcess = null)
    {
        var process = Process.Start(startInfo) ?? throw new InvalidOperationException("Failed to start process");

        useProcess?.Invoke(process);

        await process.WaitForExitAsync();

        return process;
    }
}