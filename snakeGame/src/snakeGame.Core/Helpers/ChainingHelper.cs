namespace snakeGame.Core.Helpers;

using snakeGame.Core.Abstractions;
using snakeGame.Core.Generators;
using snakeGame.Core.Shared;
using snakeGame.Core.State;
using snakeGame.Core.Playables;
using snakeGame.Core.Validators;
using snakeGame.Core.Library;

using static snakeGame.Core.Shared.Values;

public static class ChainingHelper
{
    public static Result<bool> GetValidator(
        string[] args,
        int length,
        out Arguments arguments,
        out IValidate? validator)
    {
        arguments = new();
        validator = default;

        HashMap<string, string> inputs = new();
        for (int i = 0; i < length; i += 2)
        {
            if (args.ElementAtOrDefault(i) == null || args.ElementAtOrDefault(i + 1) == null)
            {
                return ErrorInvalidArguments;
            }

            if (!inputs.TryInsert(args[i], args[i + 1]))
            {
                return ErrorInvalidArguments;
            }
        }

        DifficultyLevelValidator difficultyLevelValidator = new()
        {
            Next = null,
            Inputs = inputs,
        };

        GameModeValidator gameModeValidator = new()
        {
            Next = difficultyLevelValidator,
            Inputs = inputs,
        };

        OutputTypeValidator outputTypeValidator = new()
        {
            Next = gameModeValidator,
            Inputs = inputs,
        };

        WidthValidator widthValidator = new()
        {
            Next = outputTypeValidator,
            Inputs = inputs,
        };

        HeightValidator heightValidator = new()
        {
            Next = widthValidator,
            Inputs = inputs,
        };

        validator = heightValidator;

        return new(true, null);
    }

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