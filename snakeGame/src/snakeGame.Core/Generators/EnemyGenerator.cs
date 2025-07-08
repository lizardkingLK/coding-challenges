using snakeGame.Core.Abstractions;
using snakeGame.Core.Actors;
using snakeGame.Core.Shared;

using static snakeGame.Core.Shared.Constants;

namespace snakeGame.Core.Generators;

public class EnemyGenerator : IGenerate
{
    public IGenerate? Next { get; set; }

    public Result<bool> Generate(Manager manager)
    {
        CreateEnemy(manager);
        if (Next != null)
        {
            return Next.Generate(manager);
        }

        return new(true, null);
    }

    private static void CreateEnemy(Manager manager)
    {
        Actor? randomEnemyActor = manager.GetActor(actor => actor.State == CharSpaceBlock)
            ?? throw new Exception("error. actor value is null");

        Actor enemyActor = randomEnemyActor.Value;
        enemyActor.State = CharEnemy;

        manager.EnemyActor = enemyActor;
    }
}