using wcTool.Core;

namespace wcTool.Cli;

class Program
{
    static void Main(string[] args)
    {
      private static readonly Dictionary<string, Delegate> commands = new()
      {
          {"-c", (string filePath) => WordCount.CountBytes(filePath)},
          {"--count", (string filePath) => WordCount.CountBytes(filePath)},
          {"-l", (string filePath) => WordCount.CountLines(filePath)},
          {"--lines", (string filePath) => WordCount.CountLines(filePath)},
          {"-w", (string filePath) => WordCount.CountWords(filePath)},
          {"--words", (string filePath) => WordCount.CountWords(filePath)},
          {"-m", (string filePath) => WordCount.CountCharacters(filePath)},
          {"--characters", (string filePath) => WordCount.CountCharacters(filePath)},
      };

      static void Main(string[] args)
      {
          Result<string> response;
          int argsLength = args.Length;
          if (argsLength == 1)
          {
              response = WordCount.CountSummary(args[0]);
              Console.WriteLine(response.Data!);
              return;
          }

          if (argsLength > 0 && argsLength < 2)
          {
              Errors.WriteError(Errors.INCOMPLETE_ARGUMENTS);
              return;
          }

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