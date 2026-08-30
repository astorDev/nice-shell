namespace NiceShell;

public record Shell(string File, params string[] Flags )
{
    public static readonly Shell Bash = new("bash", "-c");
    public static readonly Shell Zsh = new("zsh", "-c");
    public static readonly Shell Cmd = new("cmd", "/c");
    public static readonly Shell PowerShell = new("powershell", "-Command");
    public static readonly Shell Pwsh = new("pwsh", "-Command");
    public static readonly Shell Sh = new("sh", "-c");

    public override string ToString()
    {
        return String.Join(";", [ File, ..Flags ]);
    }

    public static Shell Parse(string shellString)
    {
        var parts = shellString.Split(";", StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
            throw new ArgumentException("Shell string cannot be empty.", nameof(shellString));

        var file = parts[0];
        var flags = parts.Skip(1).ToArray();
        return new Shell(file, flags);
    }
}