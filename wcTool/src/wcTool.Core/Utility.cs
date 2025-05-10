namespace wcTool.Core
{
    internal static class Utility
    {
        internal static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal static Result<string> GetProcessedFilePath(string filePath)
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
                    return new Result<string>(null, Constants.ERROR_INVALID_FILE_PATH);
                }

                return new Result<string>(filePath, null);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}