namespace snakeGame.Core.Helpers;

using snakeGame.Core.Abstractions;
using snakeGame.Core.Generators;
using snakeGame.Core.Shared;
using snakeGame.Core.State;
using snakeGame.Core.Playables;

public static class ChainingHelper
{
    public static Result<bool> GetPlayable(Manager manager, IOutput output, out IPlayable playable)
    {
        Enemy enemy = new()
        {
            Next = null,
            Manager = manager,
            Output = output,
        };

        Player player = new()
        {
            Next = enemy,
            Manager = manager,
            Output = output,
        };

        playable = player;

        return new(true, null);
    }

    public static IGenerate GetGenerator(Manager manager)
    {
        PlayerGenerator playerGenerator = new()
        {
            Next = null,
            Manager = manager,
        };

        EnemyGenerator enemyGenerator = new()
        {
            Next = playerGenerator,
            Manager = manager,
        };

        WallGenerator wallGenerator = new()
        {
            Next = enemyGenerator,
            Manager = manager,
        };

        return wallGenerator;
    }
}