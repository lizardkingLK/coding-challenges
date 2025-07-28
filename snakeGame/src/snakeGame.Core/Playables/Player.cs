using snakeGame.Core.Abstractions;
using snakeGame.Core.Enums;
using snakeGame.Core.State;
using snakeGame.Core.Events;
using snakeGame.Core.Library;

using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Helpers.DirectionHelper;
using static snakeGame.Core.Helpers.ConsoleHelper;
using static snakeGame.Core.Enums.GameStateEnum;
using static snakeGame.Core.Enums.StepResultEnum;

namespace snakeGame.Core.Playables;

public class Player : IPlayable
{
    public IPlayable? Next { get; init; }

    public required Manager Manager { get; init; }

    public required IOutput Output { get; init; }

    public required GameStatePublisher Publisher { get; init; }

    public required GameModeEnum GameMode { get; init; }

    public required DynamicArray<Block> Spaces { get; init; }

    private static int _scorePerMeal;

    private static int _stepInterval;

    private int _score = 0;

    public void Play()
    {
        InitializePlay();

        if (GameMode == GameModeEnum.Automatic)
        {
            PlayAutomaticGame();
        }
        else if (GameMode == GameModeEnum.Manual)
        {
            PlayManualGame();
        }
    }

    private void InitializePlay()
    {
        DifficultyLevelEnum difficultyLevel = Manager.DifficultyLevel;
        if (Manager.GameMode == GameModeEnum.Manual)
        {
            _scorePerMeal = ScorePerMeal;
        }
        else if (difficultyLevel == DifficultyLevelEnum.Easy)
        {
            _scorePerMeal = ScorePerMeal;
            _stepInterval = StepInterval;
        }
        else if (difficultyLevel == DifficultyLevelEnum.Medium)
        {
            _scorePerMeal = ScorePerMeal * 2;
            _stepInterval = StepInterval / 2;
        }
        else if (difficultyLevel == DifficultyLevelEnum.Hard)
        {
            _scorePerMeal = ScorePerMeal * 3;
            _stepInterval = StepInterval / 4;
        }

        Output.Output();
    }

    private void PlayAutomaticGame()
    {
        DirectionEnum? direction = null;
        WriteContentToConsole(Manager.Height + 1, 0, INFO_AWAITING_KEY_PRESS, ConsoleColor.Cyan);
        while (!ReadKeyPress(direction, out direction))
        {
            WriteContentToConsole(Manager.Height + 1, 0, ERROR_INVALID_KEY_PRESSED, ConsoleColor.Red);
        }

        do
        {
            if (Console.KeyAvailable && !ReadKeyPress(direction, out direction))
            {
                WriteContentToConsole(Manager.Height + 1, 0, ERROR_INVALID_KEY_PRESSED, ConsoleColor.Red);
            }

            if (!ValidatePlayerStep(direction!.Value))
            {
                return;
            }

            Thread.Sleep(_stepInterval);
        }
        while (true);
    }

    private void PlayManualGame()
    {
        DirectionEnum? direction = null;
        WriteContentToConsole(Manager.Height + 1, 0, INFO_AWAITING_KEY_PRESS, ConsoleColor.Cyan);
        while (true)
        {
            if (!ReadKeyPress(direction, out direction))
            {
                WriteContentToConsole(Manager.Height + 1, 0, ERROR_INVALID_KEY_PRESSED, ConsoleColor.Red);
                continue;
            }

            if (!ValidatePlayerStep(direction!.Value))
            {
                return;
            }
        }
    }

    private bool ValidatePlayerStep(in DirectionEnum direction)
    {
        if (!ValidateStepToDirection(
            direction,
            out (int, int) newCordinates,
            out StepResultEnum stepResult))
        {
            WriteContentToConsoleClearLineFirst(
                Manager.Height + 1,
                0,
                string.Format(INFO_GAME_OVER, _score),
                ConsoleColor.DarkYellow);
            Publisher.Publish(new(UpdateGameOver, null));
            return false;
        }

        if (stepResult == ContinueWithNeck)
        {
            return true;
        }

        (int cordinateY, int cordinateX) = newCordinates;
        if (!ValidateMapForPlayerNewPosition(new Block()
        {
            CordinateY = cordinateY,
            CordinateX = cordinateX,
            Direction = direction,
            Type = CharPlayerHead,
        }, stepResult == ScoreAteEnemy))
        {
            WriteContentToConsoleClearLineFirst(
                Manager.Height + 1,
                0,
                string.Format(SUCCESS_GAME_COMPLETE, _score),
                ConsoleColor.Green);
            Publisher.Publish(new(UpdateGameComplete, null));
            return false;
        }

        if (stepResult == ScoreAteEnemy)
        {
            _score += _scorePerMeal;
            Next?.Play();
        }

        Output.Output();

        return true;
    }

    private bool ValidateMapForPlayerNewPosition(
        Block newPlayerHead,
        bool didPlayerEatEnemy)
    {
        if (Spaces.Size == 0)
        {
            _score += _scorePerMeal;
            return false;
        }

        Publisher.Publish(new(UpdatePlayerOldHead, null));
        Publisher.Publish(new(UpdatePlayerNewHead, newPlayerHead));
        if (didPlayerEatEnemy)
        {
            return true;
        }

        Publisher.Publish(new(UpdatePlayerTail, null));

        return true;
    }

    private bool ValidateStepToDirection(
        DirectionEnum direction,
        out (int, int) cordinates,
        out StepResultEnum stepResult)
    {
        Block currentHead = Manager.Player!.SeekFront();
        GetNextCordinate(currentHead.Cordinates, direction, out int cordinateY, out int cordinateX);
        cordinates = (cordinateY, cordinateX);

        Block[,] map = Manager.Map;
        char cordinateType = map[cordinateY, cordinateX].Type;

        if (cordinateType == CharWallBlock)
        {
            stepResult = LostAteAWall;
            return false;
        }

        if (cordinateType == CharSpaceBlock)
        {
            stepResult = ContinueWithSpace;
            return true;
        }

        if (cordinateType == CharEnemy)
        {
            stepResult = ScoreAteEnemy;
            return true;
        }

        Block neckBlock = Manager.Player!.SearchValue(0);
        if (AreSameCordinates(neckBlock.Cordinates, cordinates))
        {
            stepResult = ContinueWithNeck;
            return true;
        }

        stepResult = LostAteBody;

        return false;
    }
}