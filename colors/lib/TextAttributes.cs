namespace NiceShell;

public enum SgrTextAttributes
{
    Bold = 1,
    Dim = 2,
    NormalIntensity = 22
}

public static partial class SelectGraphicRendition
{
    public const string Bold = "\u001b[1m";
    public const string Dim = "\u001b[2m";
    public const string NormalIntensity = "\u001b[22m";
}