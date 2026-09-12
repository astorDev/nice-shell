namespace NiceShell;

public record BuildResult(RootCommand? RootCommand, Func<int>? GateResponder, Exception? BuildException)
{
    public static BuildResult RootCommandReady(RootCommand rootCommand) => new(rootCommand, null, null);
    public static BuildResult StopOnGate(Func<int> gateResponder) => new(null, gateResponder, null);
    public static BuildResult Exception(Exception ex) => new(null, null, ex);

    public int Execute(string[] args, ILogger<Cli> logger)
    {
        if (GateResponder != null)
        {
            try
            {
                return GateResponder.Invoke();
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "An error occurred while executing the gate responder.");
                return 1;
            }
        }

        if (BuildException != null)
        {
            logger.LogError(BuildException, "An error occurred while building the CLI.");
            return 1;
        }
        
        if (RootCommand != null)
        {
            try
            {
                var parsed = RootCommand.Parse(args);
                return parsed.Invoke();
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "An error occurred while invoking the root command.");
                return 1;
            }
        }

        logger.LogError("Invalid BuildResult state: no RootCommand, no GateResponder, and no BuildException.");
        return 1;
    }
}