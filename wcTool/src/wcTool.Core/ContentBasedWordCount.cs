using System.Text;

namespace wcTool.Core
{
    internal class ContentBasedWordCount
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

        internal static Result<long?> CountBytes(string content)
        {
            long byteCount = content.Length;

            return new Result<long?>(byteCount, null);
        }

        internal static Result<long?> CountLines(string content)
        {
            long lineCount = content
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .LongLength;

            return new Result<long?>(lineCount, null);
        }

        internal static Result<long?> CountWords(string content)
        {
            long wordCount = content
            .Split(Constants.WordSeparator, StringSplitOptions.RemoveEmptyEntries)
            .LongLength;

            return new Result<long?>(wordCount, null);
        }

        internal static Result<long?> CountCharacters(string content)
        {
            long charCount = content.Length;

            return new Result<long?>(charCount, null);
        }

        internal static void SetResponse(string[] arguments)
        {
            HashSet<Delegate> actions = new();
            List<Result<List<string>>> errors = new();
            List<Result<List<long?>>> results = new();

            string content = GetContent();
            if (string.IsNullOrEmpty(content))
            {
                Utility.WriteError(Constants.ERROR_INVALID_CONTENT);
                return;
            }

            HandleArguments(arguments, actions, errors);
            HandleResponses(content, actions, errors, results);
            HandleResults(results.Where(result => result.Data != null), actions);
            HandleErrors(errors.Where(error => error.Error != null));
        }

        private static string GetContent()
        {
            TextReader textReader = Console.In;
            string content = string.Empty;
            while (textReader.Peek() != -1)
            {
                content = textReader.ReadToEnd();
            }

            return content;
        }

        private static void HandleErrors(IEnumerable<Result<List<string>>> errors)
        {
            foreach (Result<List<string>> result in errors)
            {
                Utility.WriteError(result.Error!);
            }
        }

        private static void HandleResults(IEnumerable<Result<List<long?>>> results, HashSet<Delegate> actions)
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
            if (hasMultiple)
            {
                long[] aggregate = new long[actions.Count];
                foreach (Result<List<long?>> result in results)
                {
                    currentPathValues = result.Data!;
                    aggregate = aggregate.Select((item, index) => item + currentPathValues[index] ?? 0).ToArray();
                }

                totalWidth = (int)(1 + Math.Log10(aggregate.Max()));
                foreach (Result<List<long?>> result in results)
                {
                    currentPathValues = result.Data!;
                    resultBuilder.AppendLine(string.Join(
                        space,
                        currentPathValues.Select(value => $"{value ?? 0}".PadLeft(totalWidth, space))));
                }

                resultBuilder.AppendLine(string.Join(
                    space,
                    aggregate.Select(value => $"{value}".PadLeft(totalWidth, space))));
            }
            else
            {
                currentPathValues = results.First().Data!;
                long maxValue = results.SelectMany(result => result.Data!).MaxBy(result => result ?? 0) ?? 0;
                totalWidth = (int)(1 + Math.Log10(maxValue));
                resultBuilder.AppendLine(string.Join(
                    space,
                    currentPathValues.Select(value => $"{value ?? 0}".PadLeft(totalWidth, space))));
            }

            resultBuilder.AppendLine(string.Join(space, actions.Select(action =>
                $"{action.Method.Name.Replace("Count", string.Empty)[0]}".PadLeft(totalWidth, space))));

            Console.WriteLine(resultBuilder.ToString());
        }

        private static void HandleResponses(
            string content,
            HashSet<Delegate> actions,
            List<Result<List<string>>> errors,
            List<Result<List<long?>>> results)
        {
            Result<List<long?>> result = HandleResponseForContent(content, actions);
            if (result == null || result.Data == null)
            {
                errors.Add(new(null, result?.Error ?? Constants.ERROR_FILE_IN_USE));
                return;
            }

            results.Add(new Result<List<long?>>(result.Data, result.Error));
        }

        private static Result<List<long?>> HandleResponseForContent(
            string content,
            HashSet<Delegate> actions)
        {
            Dictionary<string, long?> resultMap = new();
            foreach (Delegate action in actions)
            {
                resultMap.Add(action.Method.Name, null);
            }

            Result<long?>? result;
            foreach (Delegate action in actions)
            {
                result = (Result<long?>?)action.DynamicInvoke(content);
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

            if (actions.Count == 0)
            {
                foreach (KeyValuePair<string, Delegate> command in commands)
                {
                    actions.Add(command.Value);
                }
            }
        }
    }
}