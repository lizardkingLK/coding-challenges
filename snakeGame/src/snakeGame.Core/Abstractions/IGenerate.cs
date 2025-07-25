using snakeGame.Core.Shared;
using snakeGame.Core.State;

namespace snakeGame.Core.Abstractions;

public interface IGenerate
{
    public IGenerate? Next { get; set; }

    public Manager Manager { get; init; }

    public Result<bool> Generate();
}