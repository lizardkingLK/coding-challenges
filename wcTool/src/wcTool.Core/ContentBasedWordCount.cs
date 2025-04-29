namespace wcTool.Core
{
    internal class ContentBasedWordCount
    {
        # if !DEBUG
        internal static Result<string> CountBytes()
        {
            Result<string> processedPathResult = Utility.GetProcessedFilePath(filePath);
            if (processedPathResult.Error != null)
            {
                return processedPathResult;
            }

            filePath = processedPathResult.Data!;
            byte[] bytes = File.ReadAllBytes(filePath);
            long byteCount = bytes.LongLength;
            string fileName = Path.GetFileName(filePath);

            return new Result<string>(
                string.Format(Constants.ResponseFormat, byteCount, fileName),
                null);
        }

        internal static Result<string> CountLines()
        {
            Result<string> processedPathResult = Utility.GetProcessedFilePath(filePath);
            if (processedPathResult.Error != null)
            {
                return processedPathResult;
            }

            filePath = processedPathResult.Data!;

            long lineCount;
            using (StreamReader streamReader = new(File.OpenRead(filePath)))
            {
                if (!streamReader.BaseStream.CanRead)
                {
                    return new Result<string>(null, Errors.FILE_IN_USE);
                }

                lineCount = streamReader.ReadToEnd()
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .LongLength;
            }

            string fileName = Path.GetFileName(filePath);

            return new Result<string>(
                string.Format(Constants.ResponseFormat, lineCount, fileName),
                null);
        }

        internal static Result<string> CountWords()
        {
            Result<string> processedPathResult = Utility.GetProcessedFilePath(filePath);
            if (processedPathResult.Error != null)
            {
                return processedPathResult;
            }

            filePath = processedPathResult.Data!;

            long wordCount = 0;
            using (StreamReader streamReader = new(File.OpenRead(filePath)))
            {
                if (!streamReader.BaseStream.CanRead)
                {
                    return new Result<string>(null, Errors.FILE_IN_USE);
                }

                string[] content = streamReader.ReadToEnd()
                .Split(Constants.WordSeparator, StringSplitOptions.RemoveEmptyEntries);

                wordCount = content.LongLength;
            }

            string fileName = Path.GetFileName(filePath);

            return new Result<string>(
                string.Format(Constants.ResponseFormat, wordCount, fileName),
                null);
        }

        internal static Result<string> CountCharacters()
        {
            Result<string> processedPathResult = Utility.GetProcessedFilePath(filePath);
            if (processedPathResult.Error != null)
            {
                return processedPathResult;
            }

            filePath = processedPathResult.Data!;

            long charCount = 0;
            using (StreamReader streamReader = new(File.OpenRead(filePath)))
            {
                if (!streamReader.BaseStream.CanRead)
                {
                    return new Result<string>(null, Errors.FILE_IN_USE);
                }

                while (streamReader.Read() != -1)
                {
                    charCount++;
                }
            }

            string fileName = Path.GetFileName(filePath);

            return new Result<string>(
                string.Format(Constants.ResponseFormat, charCount, fileName),
                null);
        }

        internal static Result<string> CountSummary()
        {
            Result<string> processedPathResult = Utility.GetProcessedFilePath(filePath);
            if (processedPathResult.Error != null)
            {
                return processedPathResult;
            }

            filePath = processedPathResult.Data!;

            long lineCount = 0;
            long wordCount = 0;
            long byteCount = 0;
            using (StreamReader streamReader = new(File.OpenRead(filePath)))
            {
                if (!streamReader.BaseStream.CanRead)
                {
                    return new Result<string>(null, Errors.FILE_IN_USE);
                }

                string content = streamReader.ReadToEnd();

                byteCount = File.ReadAllBytes(filePath).LongLength;
                lineCount = content.Split('\n', StringSplitOptions.RemoveEmptyEntries).LongLength;
                wordCount = content.Split(Constants.WordSeparator, StringSplitOptions.RemoveEmptyEntries).LongLength;
            }

            string fileName = Path.GetFileName(filePath);

            return new Result<string>(
                string.Format(
                    Constants.ResponseFormat,
                    string.Format("{0} {1} {2}", lineCount, wordCount, byteCount),
                    fileName),
                null);
        }

        internal static void HandleArguments(string[] args)
        {
            Result<string> response;
            int argsLength = args.Length;
            if (argsLength == 0 && string.IsNullOrEmpty(content))
            {
                response = CountSummary();
                Console.WriteLine(response.Data!);
                return;
            }

            if (argsLength == 1)
            {
                filePath = args[0];
                response = CountSummary();
                Console.WriteLine(response.Data!);
                return;
            }

            if (argsLength > 0 && argsLength < 2)
            {
                Errors.WriteError(Errors.INCOMPLETE_ARGUMENTS);
                return;
            }

            if (string.IsNullOrEmpty(args[0]) || string.IsNullOrEmpty(args[1]))
            {
                Errors.WriteError(Errors.INCOMPLETE_ARGUMENTS);
                return;
            }

            command = args[0];
            filePath = args[1];
            if (!commands.TryGetValue(command, out Delegate? commandFunction))
            {
                Errors.WriteError(Errors.INVALID_COMMANDS);
                return;
            }

            response = ((Func<string, Result<string>>)commandFunction)!(filePath);
            if (response.Data != null)
            {
                Console.WriteLine(response.Data);
                return;
            }

            Errors.WriteError(response.Error!);
        }
        # endif
    }
}