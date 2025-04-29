using wcTool.Core;

namespace wcTool.Cli;

class Program
{
    static void Main(string[] args)
    {
        WordCount.HandleArguments(args);
    }
}