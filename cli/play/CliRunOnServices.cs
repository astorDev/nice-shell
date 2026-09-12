namespace Playground;

[TestClass]
public class CliRunOnServices
{
    [TestMethod]
    public void Two()
    {
        var builder = new CliBuilder();
        builder.Services.AddSingleton<ServiceA>();
        builder.Services.AddSingleton<ServiceB>();

        var cli = builder.Build("cli-run-on-services two");

        cli.Run((ServiceA a, ServiceB b) =>
        {
            Console.WriteLine(a.GetMessage());
            Console.WriteLine(b.GetMessage());
        });
    }
}

public class ServiceA
{
    public string GetMessage() => "Hello from ServiceA!";
}

public class ServiceB
{
    public string GetMessage() => "Hello from ServiceB!";
}