using snakeGame.Core.Abstractions;
using snakeGame.Core.Library;
using snakeGame.Core.Shared;
using snakeGame.Core.State;
using snakeGame.Core.Enums;

using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Helpers.DirectionHelper;
using static snakeGame.Core.Helpers.GameBoardHelper;

namespace snakeGame.Core.Generators;

public class PlayerGenerator : IGenerate
{
    private readonly Random _random = new();

    public IGenerate? Next { get; set; }

    public Result<bool> Generate(Manager manager)
    {
        DynamicArray<Block> spaces = manager.Spaces;
        Block[,] map = manager.Map;

        Block player = UpdateSpaceBlockOut(spaces, _random.Next(0, spaces.Size));
        (int y, int x, _) = player;
        UpdateMapBlock(map, (y, x), CharPlayerHead);

        manager.Player = SelectPlayerBody(
            player,
            manager,
            (manager.Height, manager.Width));

        if (Next != null)
        {
            return Next.Generate(manager);
        }

        return new(true, null);
    }

    private static Deque<Block> SelectPlayerBody(
        Block player,
        Manager manager,
        (int, int) dimensions)
    {
        (int y, int x, _) = player;
        Deque<Block> playerBody = new(player);
        DynamicArray<Block> spaces = manager.Spaces;
        Block[,] map = manager.Map;

        Block selectedPlayerBodyBlock;
        DirectionEnum direction = GetRandomDirection();
        for (int i = 0; i < PlayerInitialLength; i++)
        {
            (y, x, direction) = SelectValidCordinate(
            (y, x, direction),
            dimensions,
            map,
            out int checkCordinateCount);

            if (checkCordinateCount == directionsLength)
            {
                break;
            }

            selectedPlayerBodyBlock = UpdateSpaceBlockOut(spaces, SelectSearchFunction(y, x)!);

            UpdateMapBlock(map, (y, x), CharPlayerBody, direction);

            playerBody.InsertToRear(selectedPlayerBodyBlock);
        }

        return playerBody;
    }

    private static (int, int, DirectionEnum) SelectValidCordinate(
        (int, int, DirectionEnum) cordinates,
        (int, int) dimensions,
        Block[,] map,
        out int checkCordinateCount)
    {
        checkCordinateCount = 0;
        (int y, int x, DirectionEnum direction) = cordinates;
        while (true)
        {
            GetNextCordinate((y, x), direction, out int cordinateY, out int cordinateX);
            if (!IsValidCordinate(cordinateY, cordinateX, dimensions, map)
            && checkCordinateCount < directionsLength)
            {
                direction = GetNextDirection(direction);
                checkCordinateCount++;
                continue;
            }

            return (cordinateY, cordinateX, direction);
        }
    }

    private static Func<Block, bool> SelectSearchFunction(int y, int x)
    {
        return space =>
        AreSameCordinates((space.CordinateY, space.CordinateX), (y, x));
    }
}