namespace NiceShell;

public enum SgrForegroundColors
{
    Black = 30,
    Red = 31,
    Green = 32,
    Yellow = 33,
    Blue = 34,
    Magenta = 35,
    Cyan = 36,
    White = 37,
    Reset = 39,
    BrightBlack = 90,
    BrightRed = 91,
    BrightGreen = 92,
    BrightYellow = 93,
    BrightBlue = 94,
    BrightMagenta = 95,
    BrightCyan = 96,
    BrightWhite = 97
}

public static partial class SelectGraphicRendition
{
    public const string Black = "\x1B[30m";
    public const string Red = "\x1B[31m";
    public const string Green = "\x1B[32m";
    public const string Yellow = "\x1B[33m";
    public const string Blue = "\x1B[34m";
    public const string Magenta = "\x1B[35m";
    public const string Cyan = "\x1B[36m";
    public const string White = "\x1B[37m";
    public const string Reset = "\x1B[39m";
    public const string BrightBlack = "\x1B[90m";
    public const string BrightRed = "\x1B[91m";
    public const string BrightGreen = "\x1B[92m";
    public const string BrightYellow = "\x1B[93m";
    public const string BrightBlue = "\x1B[94m";
    public const string BrightMagenta = "\x1B[95m";
    public const string BrightCyan = "\x1B[96m";
    public const string BrightWhite = "\x1B[97m";

    public const string RgbMaxedCyan = "\x1B[38;2;0;255;255m";
    public const string RgbMaxedRed = "\x1B[38;2;255;0;0m";
    public const string RgbMaxedGreen = "\x1B[38;2;0;255;0m";
    public const string RgbMaxedBlue = "\x1B[38;2;0;0;255m";
    public const string RgbMaxedYellow = "\x1B[38;2;255;255;0m";
    public static string ForegroundRgb(int r, int g, int b) => $"\x1B[38;2;{r};{g};{b}m";
}