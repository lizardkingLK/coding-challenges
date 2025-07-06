using snakeGame.Core.Abstractions;
using snakeGame.Core.Generators;

namespace snakeGame.Core.Helpers;

public static class GeneratorHelper
{
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

        SpaceGenerator spaceGenerator = new()
        {
            Next = enemyGenerator,
        };

        WallGenerator wallGenerator = new()
        {
            Next = spaceGenerator,
        };

        return wallGenerator;
    }
}