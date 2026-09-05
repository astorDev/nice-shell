namespace NiceShell;

public static partial class SelectGraphicRendition
{
    public const char Escape = '\u001b';
    public const char Introducer = '[';
    public static readonly string Start = new([Escape, Introducer]);
    public const char TypeLetter = 'm';

    public static string For(int code) => Start + code.ToString() + TypeLetter;
    public static string For(Enum @enumCode) => For(Convert.ToInt32(@enumCode));
}
