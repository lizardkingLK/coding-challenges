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

    internal static Result<bool> IsValidArguments(string[] args)
    {
        return args.Length > 0
        ? new Result<bool>(true, null)
        : new Result<bool>(false, ERROR_NOT_ENOUGH_ARGUMENTS);
    }

    internal static void WriteError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.White;
    }

    internal static void WriteSuccess(DateTime start, DateTime end)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(string.Format(
            SUCCESS_VALID_JSON_FOUND,
            (end - start).TotalNanoseconds));
        Console.ForegroundColor = ConsoleColor.White;
    }

    internal static bool IsValidNumber(char[] content)
    {
        return RegexForValidNumericLiteral().IsMatch(content);
    }

    internal static bool IsValidEscapeSequence(char[] content)
    {
        return RegexForEscapeSequence().IsMatch(content);
    }

    internal static bool IsValueIncluded(char[] array, char value)
    {
        int length = array.Length;
        for (int i = 0; i < length; i++)
        {
            if (array[i] == value)
            {
                return true;
            }
        }

        return false;
    }
}
