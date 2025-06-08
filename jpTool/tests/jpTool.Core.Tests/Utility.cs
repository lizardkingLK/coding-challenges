namespace jpTool.Core.Tests;

internal static class Utility
{
    internal static string GetAbsoluteFilePath(string filePath)
    {
        return Path.Combine(Directory.GetCurrentDirectory(), filePath);
    }
}