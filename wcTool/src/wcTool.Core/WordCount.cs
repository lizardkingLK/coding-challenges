namespace wcTool.Core;

public class WordCount
{
    public static void HandleArguments(string[] args)
    {
        if (!Console.IsInputRedirected)
        {
            // ContentBasedWordCount.HandleArguments(args);
        }
        else
        {
            FileBasedWordCount.GetResponse(args);
        }
    }
}
