using snakeGame.Core.Abstractions;
using snakeGame.Core.Enums;
using snakeGame.Core.State;

using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Shared.Utility;
using static snakeGame.Core.Helpers.DirectionHelper;
using static snakeGame.Core.Helpers.GameBoardHelper;
using static snakeGame.Core.Helpers.ConsoleHelper;

namespace snakeGame.Core.Updators;

public class PlayerUpdator : IPlay
{
    public IPlay? Next { get; init; }

    public required Manager Manager { get; init; }

    public required IOutput Output { get; init; }

    private static int score = 0;

    public void Play()
    {
        Output.Output(Manager);

        WriteInfo(INFO_AWAITING_KEY_PRESS);
        do
        {
            if (!ReadKeyPress(out DirectionEnum direction))
            {
                WriteError(ERROR_INVALID_KEY_PRESSED);
                continue;
            }

            if (!ValidateStepToDirection(
                direction,
                out (int, int) newCordinates,
                out StepResultEnum stepResult))
            {
                WriteInfo(INFO_GAME_OVER, score);
                return;
            }

            if (stepResult == StepResultEnum.ContinueWithNeck)
            {
                continue;
            }

            (int cordinateY, int cordinateX) = newCordinates;
            if (!ValidateMapForPlayerNewPosition(new Block()
            {
                CordinateY = cordinateY,
                CordinateX = cordinateX,
                Direction = direction,
                Type = CharPlayerHead,
            }, stepResult == StepResultEnum.ScoreAteEnemy))
            {
                WriteSuccess(SUCCESS_GAME_COMPLETE, score);
                return;
            }

            if (stepResult == StepResultEnum.ScoreAteEnemy)
            {
                score += ScorePerMeal;
                Next?.Play();
            }

            Output.Output(Manager);
        }
        while (true);
    }

    private bool ValidateMapForPlayerNewPosition(
        Block newPlayerBlock,
        bool didPlayerEatEnemy)
    {
        if (Manager.Spaces.Size == 0)
        {
            score += ScorePerMeal;
            return false;
        }

        (int y, int x, _) = Manager.Player!.SeekFront();
        (int, int) newCordinates = (y, x);
        UpdateMapBlock(Manager.Map, newCordinates, CharPlayerBody);

        Manager.Player!.InsertToFront(newPlayerBlock);
        (int cordinateY, int cordinateX, _) = newPlayerBlock;
        UpdateMapBlock(Manager.Map, (cordinateY, cordinateX), CharPlayerHead);
        UpdateSpaceBlockOut(Manager.Spaces, block => AreSameCordinates
        ((block.CordinateY, block.CordinateX), newCordinates));

        if (didPlayerEatEnemy)
        {
            return true;
        }

        Block oldPlayerTailBlock = Manager.Player!.RemoveFromRear();
        (y, x, _) = oldPlayerTailBlock;
        UpdateMapBlock(Manager.Map, (y, x), CharSpaceBlock);
        UpdateSpaceBlockIn(Manager.Spaces, oldPlayerTailBlock);

        return true;
    }

    private bool ValidateStepToDirection(
        DirectionEnum direction,
        out (int, int) cordinates,
        out StepResultEnum stepResult)
    {
        (int y, int x, _) = Manager.Player!.SeekFront();
        GetNextCordinate((y, x), direction, out int cordinateY, out int cordinateX);
        cordinates = (cordinateY, cordinateX);

        Block[,] map = Manager.Map;
        char cordinateType = map[cordinateY, cordinateX].Type;

        if (cordinateType == CharWallBlock)
        {
            stepResult = StepResultEnum.LostAteAWall;
            return false;
        }

        if (cordinateType == CharSpaceBlock)
        {
            stepResult = StepResultEnum.ContinueWithSpace;
            return true;
        }

        if (cordinateType == CharEnemy)
        {
            stepResult = StepResultEnum.ScoreAteEnemy;
            return true;
        }

        Block neckBlock = Manager.Player!.SearchValue(0);
        if (AreSameCordinates((neckBlock.CordinateY, neckBlock.CordinateX), cordinates))
        {
            stepResult = StepResultEnum.ContinueWithNeck;
            return true;
        }

        stepResult = StepResultEnum.LostAteBody;

        return false;
    }
}