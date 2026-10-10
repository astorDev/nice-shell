using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NiceShell;

public class TestLogger
{
    public static readonly ILogger Default = Create();

    public static ILogger Create(LogLevel minimumLevel = LogLevel.Trace, Action<NiceShellConsoleFormatterSettings>? configure = null)
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddLogging(builder => {
            builder.SetMinimumLevel(minimumLevel);
            builder.AddNiceShell(configure);
        });

        var serviceProvider = serviceCollection.BuildServiceProvider();
        return serviceProvider.GetRequiredService<ILogger<TestLogger>>();
    }
}