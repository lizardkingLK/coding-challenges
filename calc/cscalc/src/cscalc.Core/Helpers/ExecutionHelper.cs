namespace cscalc.Core.Helpers;

public static class ExecutionHelper
{
    public static void ExitError(string message)
    {
        Console.WriteError(message);
        Environment.Exit(1);
    }

    public static void ExitSuccess(string message)
    {
        Console.WriteError(message);
        Environment.Exit(0);
    }
}