namespace wcTool.Core;

public class WordCount
{
    public static void HandleArguments(string[] args)
    {
        if (!Console.IsInputRedirected || args.Any(arg => !arg.StartsWith('-')))
        {
            FileBasedWordCount.GetResponse(args);
            return;
        }

        // Console.WriteLine("Has no Paths");
        // ContentBasedWordCount.HandleArguments(args);
    }
}
