using Hesive;

namespace NiceShell;

public class CliApp
{
    public static AppBuilder Builder()
    {
        var builder = new AppBuilder();

        return builder;
    }
}

// public class AutomaticCliGate(IServiceProvider services, ILogger<AutomaticCliGate> logger) : ICliGate
// {
//     public CliGateResult Process(string[] args)
//     {
//         logger.LogTrace("Trying to resolve RootCommand directly from services");
//         services.GetService<RootCommand>();
//     }
// }