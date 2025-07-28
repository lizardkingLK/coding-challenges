using snakeGame.Core.Abstractions;
using snakeGame.Core.Enums;
using snakeGame.Core.Shared;
using snakeGame.Core.State;
using snakeGame.Core.Library;
using snakeGame.Core.Output.Console;
using snakeGame.Core.Output.Document;

using static snakeGame.Core.Helpers.ConsoleHelper;

namespace snakeGame.Core.Helpers;

public static class OutputHelper
{
    private static readonly HashMap<OutputTypeEnum, IOutput> _outputMap = new();

    static OutputHelper()
    {
        _outputMap.Insert(OutputTypeEnum.Console, new ConsoleOutput());
        _outputMap.Insert(OutputTypeEnum.TextFile, new TextFileOutput());
    }

    public static Result<bool> GetOutput(Manager manager, out IOutput? output)
    {
        OutputTypeEnum outputType = manager.OutputType;
        if (!_outputMap.TryGetValue(outputType, out output))
        {
            return new(false, null);
        }

        output!.Manager = manager;
        manager.Output = output;

        InitializeConsole();

        return new(true, null);
    }
}