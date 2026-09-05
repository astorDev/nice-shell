namespace NiceShell;

public enum SgrTextAttributes
{
    Bold = 1,
    Dim = 2,
    NormalIntensity = 22
}

public static partial class SelectGraphicRendition
{
    public const string Bold = "\x1B[1m";
    public const string Dim = "\x1B[2m";
    public const string NormalIntensity = "\x1B[22m";
}