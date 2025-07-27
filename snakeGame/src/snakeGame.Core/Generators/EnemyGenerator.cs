using snakeGame.Core.Abstractions;
using snakeGame.Core.Enums;

using snakeGame.Core.Shared;
using snakeGame.Core.State;

namespace snakeGame.Core.Generators;

public class EnemyGenerator : IGenerate
{
    public required Manager Manager { get; init; }

    public IGenerate? Next { get; set; }

    public Result<bool> Generate()
    {
        Manager.Publisher!.Publish(new(GameStateEnum.CreateEnemy, null));

        return Next?.Generate() ?? new(true, null);
    }
}