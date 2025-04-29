namespace wcTool.Core
{
    internal class FileBasedWordCount
    {
        private static readonly Dictionary<string, Delegate> commands = new()
        {
            {"c", CountBytes},
            {"bytes", CountBytes},
            {"l", CountLines},
            {"lines", CountLines},
            {"w", CountWords},
            {"words", CountWords},
            {"m", CountCharacters},
            {"characters", CountCharacters},
        };

        internal static Result<string> CountBytes(string filePath)
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            long byteCount = bytes.LongLength;
            string fileName = Path.GetFileName(filePath);

            return new Result<string>(
                string.Format(Constants.ResponseFormat, byteCount, fileName),
                null);
        }

        internal static Result<string> CountLines(string filePath)
        {
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

        internal static Result<string> CountWords(string filePath)
        {
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

        internal static Result<string> CountCharacters(string filePath)
        {
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

        internal static Result<string> CountSummary(string filePath)
        {
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

        internal static void GetResponse(string[] arguments)
        {
            HashSet<Delegate> actions = new();
            List<string> paths = new();
            List<Result<string>> results = new();

            HandleArguments(arguments, actions, paths, results);
            HandleResponses(actions, paths, results);
            HandleResults(results);
        }

        private static void HandleResults(List<Result<string>> results)
        {
            foreach (Result<string> result in results.Where(result => result.Data != null))
            {
                Console.WriteLine(result.Data);
            }

            foreach (Result<string> result in results.Where(result => result.Error != null))
            {
                Errors.WriteError(result.Error!);
            }
        }

        private static void HandleResponses(
            HashSet<Delegate> actions,
            List<string> paths,
            List<Result<string>> results)
        {
            foreach (string path in paths)
            {
                HandleResponseForPath(path, actions, results);
            }
        }

        private static void HandleResponseForPath(
            string path,
            HashSet<Delegate> actions,
            List<Result<string>> results)
        {
            Result<string> processedPathResult = Utility.GetProcessedFilePath(path);
            if (processedPathResult.Error != null)
            {
                results.Add(new Result<string>(null, string.Format(Errors.INVALID_FILE_PATH, path)));
                return;
            }

            path = processedPathResult.Data!;

            foreach (Delegate action in actions)
            {
                results.Add((Result<string>)action.DynamicInvoke(path)!);
            }
        }

        private static void HandleArguments(
            string[] arguments,
            HashSet<Delegate> actions,
            List<string> paths,
            List<Result<string>> results)
        {
            string commandKey;

            IEnumerable<string> inputs = arguments.Where(argument => argument.StartsWith("--"));
            foreach (string input in inputs)
            {
                commandKey = input.Replace("--", string.Empty);
                if (commands.TryGetValue(commandKey, out Delegate? command))
                {
                    actions.Add(command);
                    continue;
                }

                results.Add(new Result<string>(null, string.Format(Errors.INVALID_COMMANDS, commandKey)));
            }

            arguments = arguments.Except(inputs).ToArray();
            inputs = arguments.Where(argument => argument.StartsWith("-"));
            foreach (char commandCharKey in inputs.SelectMany(argument => argument.Replace("-", string.Empty).ToCharArray()))
            {
                if (commands.TryGetValue(commandCharKey.ToString(), out Delegate? command))
                {
                    actions.Add(command);
                    continue;
                }

                results.Add(new Result<string>(null, string.Format(Errors.INVALID_COMMANDS, commandCharKey)));
            }

            paths.AddRange(arguments.Except(inputs).ToArray());
        }
    }
}