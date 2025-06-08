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

    internal static bool IsValidNullLiteral(string content, long length)
    {
        return content == Null && length == 4;
    }

    internal static bool IsValidTrueLiteral(string content, long length)
    {
        return content == True && length == 4;
    }

    internal static bool IsValidFalseLiteral(string content, long length)
    {
        return content == False && length == 5;
    }

    internal static bool IsValidScientificNumber(string content)
    {
        return RegexForMatchScientificNumber().IsMatch(content);
    }

    internal static bool IsValidQuotedLine(string content)
    {
        return RegexForMatchQuotedLineOfText().IsMatch(content);
    }
}
