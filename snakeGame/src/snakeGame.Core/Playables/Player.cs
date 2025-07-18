using snakeGame.Core.Abstractions;
using snakeGame.Core.Enums;
using snakeGame.Core.State;

using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Shared.Utility;
using static snakeGame.Core.Helpers.DirectionHelper;
using static snakeGame.Core.Helpers.GameBoardHelper;
using static snakeGame.Core.Helpers.ConsoleHelper;

namespace snakeGame.Core.Playables;

public class Player : IPlayable
{
    public IPlayable? Next { get; init; }

    public required Manager Manager { get; init; }

    public required IOutput Output { get; init; }

    private static int score = 0;

    public void Play()
    {
        Output.Output();

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

            Output.Output();
        }
        while (true);
    }

    private bool ValidateMapForPlayerNewPosition(
        Block newPlayerHeadBlock,
        bool didPlayerEatEnemy)
    {
        if (Manager.Spaces.Size == 0)
        {
            score += ScorePerMeal;
            return false;
        }

        (int y, int x) = Manager.Player!.SeekFront().Cordinates;
        (int, int) newCordinates = (y, x);
        UpdateMapBlock(Manager.Map, newCordinates, CharPlayerBody);

        Manager.Player!.InsertToFront(newPlayerHeadBlock);
        UpdateMapBlock(Manager.Map, newPlayerHeadBlock.Cordinates, CharPlayerHead);
        UpdateSpaceBlockOut(Manager.Spaces, block =>
        AreSameCordinates(block.Cordinates, newCordinates));

        if (didPlayerEatEnemy)
        {
            return true;
        }

        Block oldPlayerTailBlock = Manager.Player!.RemoveFromRear();
        UpdateMapBlock(Manager.Map, oldPlayerTailBlock.Cordinates, CharSpaceBlock);
        UpdateSpaceBlockIn(Manager.Spaces, oldPlayerTailBlock);

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