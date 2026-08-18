using CliWrap;

namespace NiceShell;

public record Shell(string File, string Flag)
{
    public static readonly Shell Bash = new("/bin/bash", "-c");
    public static readonly Shell Zsh = new("/bin/zsh", "-c");
    public static readonly Shell Cmd = new("cmd.exe", "/c");
    public static readonly Shell PowerShell = new("powershell.exe", "-Command");
    public static readonly Shell Sh = new("/bin/sh", "-c");

    public Command Proxy(string rawCommand)
    {
        return Cli.Wrap(File)
            .WithArguments([Flag, rawCommand]);
    }
}