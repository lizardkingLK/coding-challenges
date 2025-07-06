using snakeGame.Core.Actors;

namespace snakeGame.Core.Abstractions;

public interface IGenerate
{
    public IGenerate? Next { get; set; }

    public Result<bool> Generate(int height, int width, Actor[][] actors);
}