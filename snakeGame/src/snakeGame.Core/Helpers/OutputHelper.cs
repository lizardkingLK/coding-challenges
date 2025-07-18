using snakeGame.Core.Abstractions;
using snakeGame.Core.Enums;
using snakeGame.Core.Output;
using snakeGame.Core.Shared;
using snakeGame.Core.State;
using snakeGame.Core.Library;

namespace snakeGame.Core.Helpers;

public static class OutputHelper
{
    private static readonly HashMap<OutputTypeEnum, IOutput> outputMap = new();

    static OutputHelper()
    {
        outputMap.Insert(OutputTypeEnum.Console, new ConsoleOutput());
        outputMap.Insert(OutputTypeEnum.StreamWriterConsole, new StreamWriterConsoleOutput());
        outputMap.Insert(OutputTypeEnum.StringBuilderConsole, new StringBuilderConsoleOutput());
        outputMap.Insert(OutputTypeEnum.TextFile, new TextFileOutput());
    }

    public static Result<bool> GetOutput(Manager manager, out IOutput? output)
    {
        OutputTypeEnum outputType = manager.OutputType;
        if (outputMap.TryGetValue(outputType, out _, out output))
        {
            output!.Manager = manager;
            return new(true, null);
        }

        return new(false, null);
    }
}