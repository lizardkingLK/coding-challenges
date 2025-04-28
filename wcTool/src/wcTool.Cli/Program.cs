using wcTool.Core;

namespace wcTool.Cli;

class Program
{
    private static readonly Dictionary<string, Delegate> commands = new()
    {
        {"-c", (string filePath) => WordCount.GetFileNameAndBytes(filePath)},
        {"--count", (string filePath) => WordCount.GetFileNameAndBytes(filePath)},
        {"-l", (string filePath) => WordCount.GetFileNameAndLineCount(filePath)},
        {"--lines", (string filePath) => WordCount.GetFileNameAndLineCount(filePath)},
    };

    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Errors.WriteError(Errors.INCOMPLETE_ARGUMENTS);
            return;
        }

        Result<string> response;
        string commandKey;
        string commandValue;
        foreach (string[] keyValue in args.Chunk(2))
        {
            commandKey = keyValue[0];
            commandValue = keyValue[1];
            if (string.IsNullOrEmpty(commandKey) || string.IsNullOrEmpty(commandValue))
            {
                Errors.WriteError(Errors.INCOMPLETE_ARGUMENTS);
                break;
            }

            if (!commands.TryGetValue(commandKey, out Delegate? commandFunction))
            {
                Errors.WriteError(Errors.INVALID_COMMANDS);
                break;
            }

            response = ((Func<string, Result<string>>)commandFunction)!(commandValue);
            if (response.Data != null)
            {
                Console.WriteLine(response.Data);
                break;
            }

            Errors.WriteError(response.Error!);
        }
    }
}
