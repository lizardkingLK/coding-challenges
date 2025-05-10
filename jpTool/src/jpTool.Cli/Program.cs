using jpTool.Core;

namespace jpTool.Cli;

class Program
{
    static void Main(string[] args)
    {
        JsonParser.ProcessValidation(args);
    }
}
