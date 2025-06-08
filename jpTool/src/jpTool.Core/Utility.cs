namespace jpTool.Core;

using static Constants;
using static RegularExpressions;

internal static class Utility
{
    internal static Result<string> ProcessFilePath(string filePath)
    {
        try
        {
            string filePathArgument = filePath;
            string currentDirectory = Directory.GetCurrentDirectory();
            bool isPathFullyQualified = Path.IsPathFullyQualified(filePath);
            bool hasExtension = Path.HasExtension(filePath);

            if (!isPathFullyQualified)
            {
                filePath = Path.GetFullPath(Path.Join(currentDirectory, filePath));
            }

            if (!hasExtension)
            {
                string? matchingFirstFile = Directory
                .GetFiles(currentDirectory, "*", SearchOption.TopDirectoryOnly)
                .Select(matchingFilePath => Path.GetFileName(matchingFilePath))
                .FirstOrDefault(fileName => fileName.StartsWith(
                    filePathArgument,
                    StringComparison.CurrentCultureIgnoreCase));

                filePath = Path.Join(currentDirectory, matchingFirstFile ?? string.Empty);
            }

            if (!File.Exists(filePath))
            {
                return new Result<string>(null, ERROR_INVALID_FILE_PATH);
            }

            return new Result<string>(filePath, null);
        }
        catch (Exception)
        {
            throw;
        }
    }

    internal static string ProcessRawJsonContent(string content)
    {
        return RegexForSpecialAndWhiteSpace().Replace(content, string.Empty);
    }

    internal static Result<bool> IsValidArguments(string[] args)
    {
        return args.Length > 0
        ? new Result<bool>(true, null)
        : new Result<bool>(false, ERROR_NOT_ENOUGH_ARGUMENTS);
    }

    internal static void WriteError(string error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(error);
        Console.ForegroundColor = ConsoleColor.White;
    }

    internal static void WriteSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.White;
    }

    internal static bool IsValidNumber(string content)
    {
        return RegexForValidNumericLiteral().IsMatch(content);
    }
}
