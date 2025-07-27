namespace snakeGame.Core.Helpers;

using snakeGame.Core.Abstractions;
using snakeGame.Core.Generators;
using snakeGame.Core.Shared;
using snakeGame.Core.State;
using snakeGame.Core.Playables;

public static class ChainingHelper
{
    public static Result<bool> GetPlayable(Manager manager, out IPlayable playable)
    {
        Enemy enemy = new()
        {
            Next = null,
            Manager = manager,
            Output = manager.Output!,
        };

        Player player = new()
        {
            Next = enemy,
            Manager = manager,
            Output = manager.Output!,
            GameMode = manager.GameMode!,
            Publisher = manager.Publisher!,
            Spaces = manager.Spaces!,
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