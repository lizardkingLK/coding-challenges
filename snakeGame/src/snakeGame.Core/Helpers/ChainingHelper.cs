namespace snakeGame.Core.Helpers;

using snakeGame.Core.Abstractions;
using snakeGame.Core.Generators;
using snakeGame.Core.Shared;
using snakeGame.Core.State;
using snakeGame.Core.Updators;

public static class ChainingHelper
{
    public static Result<bool> GetPlayable(Manager manager, IOutput output, out IPlay playable)
    {
        EnemyUpdator enemyUpdator = new()
        {
            Next = null,
            Manager = manager,
            Output = output,
        };

        PlayerUpdator playerUpdator = new()
        {
            Next = enemyUpdator,
            Manager = manager,
            Output = output,
        };

        playable = playerUpdator;

        return new(true, null);
    }

    public static IGenerate GetGenerator()
    {
        PlayerGenerator playerGenerator = new()
        {
            Next = null,
        };

        EnemyGenerator enemyGenerator = new()
        {
            Next = playerGenerator,
        };

        WallGenerator wallGenerator = new()
        {
            Next = enemyGenerator,
        };

        return wallGenerator;
    }
}