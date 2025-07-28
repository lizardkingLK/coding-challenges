using snakeGame.Core.Abstractions;
using snakeGame.Core.State;

using static snakeGame.Core.Helpers.ConsoleHelper;

namespace snakeGame.Core.Output.Console;

public class ConsoleOutput : IOutput
{
    public Manager? Manager { get; set; }

    public void Output(GameState? state = null)
    {
        if (!state.HasValue)
        {
            return;
        }

        Block? nullableBlock = state.Value.Data;
        if (!nullableBlock.HasValue)
        {
            return;
        }

        (int y, int x, _, char type) = nullableBlock.Value;

        WriteContentToConsole(y, x, type, GetColorForCharacter(type));
    }

    public void Stream(GameState state)
    {
        Output(state);
    }
}