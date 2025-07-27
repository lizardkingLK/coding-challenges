using snakeGame.Core.Abstractions;
using snakeGame.Core.Enums;
using snakeGame.Core.State;
using snakeGame.Core.Events;
using snakeGame.Core.Library;

using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Shared.Utility;
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

    private static int _score = 0;

    public void Play()
    {
        Output.Output();
        if (GameMode == GameModeEnum.Automatic)
        {
            PlayAutomaticGame();
        }
        else if (GameMode == GameModeEnum.Manual)
        {
            PlayManualGame();
        }
    }

    private void PlayAutomaticGame()
    {
        DirectionEnum? direction = null;
        WriteInfo(INFO_AWAITING_KEY_PRESS);
        while (!ReadKeyPress(direction, out direction))
        {
            WriteError(ERROR_INVALID_KEY_PRESSED);
        }

        do
        {
            if (Console.KeyAvailable && !ReadKeyPress(direction, out direction))
            {
                WriteError(ERROR_INVALID_KEY_PRESSED);
            }

            if (!ValidatePlayerStep(direction!.Value))
            {
                return;
            }

            Thread.Sleep(StepInterval);
        }
        while (true);
    }

    private void PlayManualGame()
    {
        DirectionEnum? direction = null;
        WriteInfo(INFO_AWAITING_KEY_PRESS);
        while (true)
        {
            if (!ReadKeyPress(direction, out direction))
            {
                WriteError(ERROR_INVALID_KEY_PRESSED);
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
            WriteInfo(INFO_GAME_OVER, _score);
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
            WriteSuccess(SUCCESS_GAME_COMPLETE, _score);
            return false;
        }

        if (stepResult == ScoreAteEnemy)
        {
            _score += ScorePerMeal;
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
            _score += ScorePerMeal;
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