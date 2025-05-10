using System.Text;

namespace wcTool.Core
{
    public class FileBasedWordCount
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

        public static Result<long?> CountBytes(string filePath)
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            long byteCount = bytes.LongLength;

            return new Result<long?>(byteCount, null);
        }

        public static Result<long?> CountLines(string filePath)
        {
            long lineCount;
            using (StreamReader streamReader = new(File.OpenRead(filePath)))
            {
                if (!streamReader.BaseStream.CanRead)
                {
                    return new Result<long?>(null, Constants.ERROR_FILE_IN_USE);
                }

                lineCount = streamReader.ReadToEnd()
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .LongLength;
            }

            return new Result<long?>(lineCount, null);
        }

        internal static Result<long?> CountWords(string filePath)
        {
            long wordCount = 0;
            using (StreamReader streamReader = new(File.OpenRead(filePath)))
            {
                if (!streamReader.BaseStream.CanRead)
                {
                    return new Result<long?>(null, Constants.ERROR_FILE_IN_USE);
                }

                string[] content = streamReader.ReadToEnd()
                .Split(Constants.WordSeparator, StringSplitOptions.RemoveEmptyEntries);

                wordCount = content.LongLength;
            }

            return new Result<long?>(wordCount, null);
        }

        internal static Result<long?> CountCharacters(string filePath)
        {
            long charCount = 0;
            using (StreamReader streamReader = new(File.OpenRead(filePath)))
            {
                if (!streamReader.BaseStream.CanRead)
                {
                    return new Result<long?>(null, Constants.ERROR_FILE_IN_USE);
                }

                while (streamReader.Read() != -1)
                {
                    charCount++;
                }
            }

            return new Result<long?>(charCount, null);
        }

        internal static void SetResponse(string[] arguments)
        {
            HashSet<Delegate> actions = new();
            List<string> paths = new();
            List<Result<List<string>>> errors = new();
            List<Result<(List<long?>, string)>> results = new();

            HandleArguments(arguments, actions, paths, errors);
            HandleResponses(actions, paths, errors, results);
            HandleResults(results.Where(result => result.Data.Item1 != null), actions);
            HandleConstants(errors.Where(error => error.Error != null));
        }

        private static void HandleConstants(IEnumerable<Result<List<string>>> errors)
        {
            foreach (Result<List<string>> result in errors)
            {
                Utility.WriteError(result.Error!);
            }
        }

        private static void HandleResults(IEnumerable<Result<(List<long?>, string)>> results, HashSet<Delegate> actions)
        {
            if (!results.Any())
            {
                return;
            }

            int totalWidth;
            bool hasMultiple = results.Count() > 1;
            char space = ' ';
            StringBuilder resultBuilder = new();
            List<long?> currentPathValues;
            string currentPath;
            if (hasMultiple)
            {
                long[] aggregate = new long[actions.Count];
                foreach (Result<(List<long?>, string)> result in results)
                {
                    (currentPathValues, currentPath) = result.Data;
                    aggregate = aggregate.Select((item, index) => item + currentPathValues[index] ?? 0).ToArray();
                }

                totalWidth = (int)(1 + Math.Log10(aggregate.Max()));
                foreach (Result<(List<long?>, string)> result in results)
                {
                    (currentPathValues, currentPath) = result.Data;
                    resultBuilder.AppendLine(string.Format(
                        "{0} {1}",
                        string.Join(
                        space,
                        currentPathValues.Select(value => $"{value ?? 0}".PadLeft(totalWidth, space))), currentPath));
                }

                resultBuilder.AppendLine(string.Format(
                    "{0} Total",
                    string.Join(
                    space,
                    aggregate.Select(value => $"{value}".PadLeft(totalWidth, space)))));
            }
            else
            {
                (currentPathValues, currentPath) = results.First().Data;
                long maxValue = results.SelectMany(result => result.Data!.Item1).MaxBy(result => result ?? 0) ?? 0;
                totalWidth = (int)(1 + Math.Log10(maxValue));
                resultBuilder.AppendLine(string.Format(
                    "{0} {1}",
                    string.Join(
                    space,
                    currentPathValues.Select(value => $"{value ?? 0}".PadLeft(totalWidth, space))),
                    currentPath));
            }

            resultBuilder.AppendLine(string.Join(space, actions.Select(action =>
                $"{action.Method.Name.Replace("Count", string.Empty)[0]}".PadLeft(totalWidth, space))));

            Console.WriteLine(resultBuilder.ToString());
        }

        private static void HandleResponses(
            HashSet<Delegate> actions,
            List<string> paths,
            List<Result<List<string>>> errors,
            List<Result<(List<long?>, string)>> results)
        {
            Result<List<long?>> result;
            foreach (string path in paths)
            {
                result = HandleResponseForPath(path, actions);
                if (result == null || result.Data == null)
                {
                    errors.Add(new(null, result?.Error ?? Constants.ERROR_FILE_IN_USE));
                    continue;
                }

                results.Add(new Result<(List<long?>, string)>((result.Data, path), result.Error));
            }
        }

        private static Result<List<long?>> HandleResponseForPath(
            string path,
            HashSet<Delegate> actions)
        {
            Result<string> processedPathResult = Utility.GetProcessedFilePath(path);
            if (processedPathResult.Error != null)
            {
                return new Result<List<long?>>(null, string.Format(Constants.ERROR_INVALID_FILE_PATH, path));
            }

            path = processedPathResult.Data!;

            Dictionary<string, long?> resultMap = new();
            foreach (Delegate action in actions)
            {
                resultMap.Add(action.Method.Name, null);
            }

            Result<long?>? result;
            foreach (Delegate action in actions)
            {
                result = (Result<long?>?)action.DynamicInvoke(path);
                if (result == null)
                {
                    return new Result<List<long?>>(null, Constants.ERROR_FILE_IN_USE);
                }

                if (result.Error != null)
                {
                    return new Result<List<long?>>(null, result.Error);
                }

                resultMap[action.Method.Name] = result.Data;
            }

            return new Result<List<long?>>(resultMap.Values.ToList(), null);
        }

        private static void HandleArguments(
            string[] arguments,
            HashSet<Delegate> actions,
            List<string> paths,
            List<Result<List<string>>> errors)
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

                errors.Add(new Result<List<string>>(null, string.Format(Constants.ERROR_INVALID_COMMANDS, commandKey)));
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

                errors.Add(new Result<List<string>>(null, string.Format(Constants.ERROR_INVALID_COMMANDS, commandCharKey)));
            }

            paths.AddRange(arguments.Except(inputs).ToArray());

            if (paths.Count > 0 && actions.Count == 0)
            {
                foreach (KeyValuePair<string, Delegate> command in commands)
                {
                    actions.Add(command.Value);
                }
            }
        }
    }
}