using snakeGame.Core.Abstractions;
using snakeGame.Core.Actors;

using static snakeGame.Core.Shared.Constants;

namespace snakeGame.Core.Generators;

public class EnemyGenerator : IGenerate
{
    public IGenerate? Next { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Result<bool> Generate(int height, int width, Actor[][]? actors = null)
    {
        throw new NotImplementedException();
    }
}