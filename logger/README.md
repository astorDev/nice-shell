# NiceShell Logger

Extension of Microsoft's logging framework with NiceShell basic capabilities:

- `LogTrace`, `LogDebug`, `LogInformation` -> Writes log line to stderr in cyan. (like `NiceShell.Console.WriteLogLine`)
- `LogWarning` -> Writes log line to stderr in yellow. Optionally prefixed with warning sign. Inspiration: `NiceShell.Console.WriteWarningLine`
- `LogError` -> Writes log line to stderr in red. Inspiration: `NiceShell.Console.WriteErrorLine`
