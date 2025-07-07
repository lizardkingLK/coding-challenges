using snakeGame.Core.Actors;
using snakeGame.Core.Shared;

namespace snakeGame.Core.Abstractions;

public interface IGenerate
{
    public IGenerate? Next { get; set; }

    public Result<bool> Generate(Manager manager);
}