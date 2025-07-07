using snakeGame.Core.Abstractions;
using snakeGame.Core.Actors;
using snakeGame.Core.Shared;

namespace snakeGame.Core.Generators;

public class EnemyGenerator : IGenerate
{
    public IGenerate? Next { get; set; }

    public Result<bool> Generate(Manager manager)
    {
        manager.CreateEnemy();
        if (Next != null)
        {
            return Next.Generate(manager);
        }

        return new(true, null);
    }
}