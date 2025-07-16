using snakeGame.Core.Abstractions;
using snakeGame.Core.Library;
using snakeGame.Core.Shared;
using snakeGame.Core.State;
using snakeGame.Core.Enums;

using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Helpers.DirectionHelper;

namespace snakeGame.Core.Generators;

public class PlayerGenerator : IGenerate
{
    private readonly Random _random = new();

    public IGenerate? Next { get; set; }

    public Result<bool> Generate(Manager manager)
    {
        DynamicArray<Block> spaces = manager.Spaces;
        Block[,] map = manager.Map;

        Block player = spaces.Remove(_random.Next(0, spaces.Size));
        (int y, int x, _) = player;
        map[y, x].Type = CharPlayerHead;

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

    private static Deque<Block>? SelectPlayerBody(
        Block player,
        Manager manager,
        (int, int) dimensions)
    {
        (int y, int x, _) = player;
        DynamicArray<Block> spaces = manager.Spaces;
        Block[,] map = manager.Map;

        Deque<Block> playerBody = new(player);
        Block selectedPlayerBodyBlock;
        DirectionEnum direction = GetRandomDirection();
        for (int i = 0; i < PlayerInitialLength; i++)
        {
            (y, x, direction) = SelectValidCordinate(
                (y, x, direction),
                dimensions,
                map,
                out int checkCordinateCount);

            ArgumentOutOfRangeException.ThrowIfEqual(checkCordinateCount, directionsLength);

            selectedPlayerBodyBlock = spaces.Remove(SelectSearchFunction(y, x))!;
            map[y, x].Type = CharPlayerBody;
            map[y, x].Direction = direction;

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
        return space => space.CordinateY == y && space.CordinateX == x;
    }
}