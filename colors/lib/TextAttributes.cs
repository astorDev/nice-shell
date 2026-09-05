namespace NiceShell;

public enum SgrTextAttributes
{
    Dim = 2,
    NormalIntensity = 22
}

public static partial class SelectGraphicRendition
{
    public const string Dim = "\u001b[2m";
    public const string NormalIntensity = "\u001b[22m";
}