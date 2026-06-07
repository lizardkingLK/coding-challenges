namespace ccct.Core.Tests.Shared;

internal static class Utility
{
    internal static string GetAbsolutePath(string filePath)
    {
        return Path.Combine(AppContext.BaseDirectory, filePath);
    }
}