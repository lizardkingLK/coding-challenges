using snakeGame.Core.State;

namespace snakeGame.Core.Abstractions;

public interface IOutput
{
    public Manager? Manager { get; set; }

    public void Stream(GameState state);

    public void Output(GameState? state = null);
}