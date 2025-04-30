namespace wcTool.Core;

public class WordCount
{
    public static void HandleArguments(string[] args)
    {
        if (!Console.IsInputRedirected || args.Any(arg => !arg.StartsWith('-')))
        {
            FileBasedWordCount.SetResponse(args);
            return;
        }

        ContentBasedWordCount.SetResponse(args);
    }
}
