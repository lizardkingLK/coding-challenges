namespace snakeGame.Core.Helpers;

using snakeGame.Core.Abstractions;
using snakeGame.Core.Display;
using snakeGame.Core.Generators;

public static class ChainingHelper
{
    public static IDisplay GetFileDisplay()
    {
        return new FileDisplay();
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