using snakeGame.Core.Abstractions;
using snakeGame.Core.Enums;
using snakeGame.Core.Output;

namespace snakeGame.Core.Helpers;

public static class OutputHelper
{
    public static IOutput GetOutput(OutputTypeEnum outputType)
    {
        if (outputType == OutputTypeEnum.Console)
        {
            return new ConsoleOutput();
        }

        return new TextFileOutput();
    }
}