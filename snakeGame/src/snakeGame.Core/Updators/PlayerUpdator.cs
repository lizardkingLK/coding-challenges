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
        StepResultEnum stepResult;
        do
        {
            if (!ReadKeyPress(out DirectionEnum direction))
            {
                WriteError(ERROR_INVALID_KEY_PRESSED);
                continue;
            }

            stepResult = ValidateStepToDirection(direction, out (int, int) newCordinates);
            if (stepResult == StepResultEnum.LostAteAWall
            || stepResult == StepResultEnum.LostAteBody)
            {
                WriteInfo(INFO_GAME_OVER, score);
                return;
            }

            if (stepResult == StepResultEnum.ContinueWithNeck)
            {
                continue;
            }

            UpdateStepResult(newCordinates, direction);

            if (stepResult == StepResultEnum.ScoreAteEnemy)
            {
                score += ScorePerMeal;
                Next?.Play();
            }
        }
        while (true);
    }

    private void UpdateStepResult((int, int) newCordinates, DirectionEnum direction)
    {
        (int cordinateY, int cordinateX) = newCordinates;
        UpdateMapForPlayerNewPosition(new Block()
        {
            CordinateY = cordinateY,
            CordinateX = cordinateX,
            Direction = direction,
            Type = CharPlayerHead,
        });

        Output.Output(Manager);
    }

    private StepResultEnum ValidateStepToDirection(DirectionEnum direction, out (int, int) cordinates)
    {
        (int y, int x, _) = Manager.Player!.SeekFront();
        GetNextCordinate((y, x), direction, out int cordinateY, out int cordinateX);
        cordinates = (cordinateY, cordinateX);

        Block[,] map = Manager.Map;
        char cordinateType = map[cordinateY, cordinateX].Type;

        if (cordinateType == CharWallBlock)
        {
            return StepResultEnum.LostAteAWall;
        }

        if (cordinateType == CharSpaceBlock)
        {
            return StepResultEnum.ContinueWithSpace;
        }

        if (cordinateType == CharEnemy)
        {
            return StepResultEnum.ScoreAteEnemy;
        }

        Block neckBlock = Manager.Player!.SearchValue(0);
        if (AreSameCordinates((neckBlock.CordinateY, neckBlock.CordinateX), cordinates))
        {
            return StepResultEnum.ContinueWithNeck;
        }

        return StepResultEnum.LostAteBody;
    }

    private void UpdateMapForPlayerNewPosition(Block newPlayerBlock)
    {
        (int y, int x, _) = Manager.Player!.SeekFront();
        (int, int) newCordinates = (y, x);
        UpdateMapBlock(Manager.Map, newCordinates, CharPlayerBody);

        Manager.Player!.InsertToFront(newPlayerBlock);
        (int cordinateY, int cordinateX, _) = newPlayerBlock;
        UpdateMapBlock(Manager.Map, (cordinateY, cordinateX), CharPlayerHead);
        UpdateSpaceBlockOut(Manager.Spaces, block => AreSameCordinates
        ((block.CordinateY, block.CordinateX), newCordinates));

        Block oldPlayerTailBlock = Manager.Player!.RemoveFromRear();
        (y, x, _) = oldPlayerTailBlock;
        UpdateMapBlock(Manager.Map, (y, x), CharSpaceBlock);
        UpdateSpaceBlockIn(Manager.Spaces, oldPlayerTailBlock);
    }
}