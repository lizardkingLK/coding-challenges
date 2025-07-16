using snakeGame.Core.Abstractions;
using snakeGame.Core.Enums;
using snakeGame.Core.Output;
using snakeGame.Core.Shared;

namespace snakeGame.Core.Helpers;

public static class OutputHelper
{
    public static Result<bool> GetOutput(OutputTypeEnum outputType, out IOutput output)
    {
        if (outputType == OutputTypeEnum.Console)
        {
            output = new ConsoleOutput();
            return new(true, null);
        }

        output = new TextFileOutput();

        return new(true, null);
    }
}