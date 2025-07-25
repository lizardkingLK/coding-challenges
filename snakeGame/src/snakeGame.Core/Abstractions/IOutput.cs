using snakeGame.Core.State;

namespace snakeGame.Core.Abstractions;

public interface IOutput
{
    public Manager? Manager { get; set; }

    public void Output();
}