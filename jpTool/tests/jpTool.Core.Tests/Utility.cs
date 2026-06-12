namespace jpTool.Core.Tests;

internal static class Utility
{
    internal static string GetAbsoluteFilePath(string filePath)
    {
        return Path.GetFullPath(
            Path.Combine(
                Directory.GetCurrentDirectory(),
                filePath.Replace('\\', '/')));
    }
}