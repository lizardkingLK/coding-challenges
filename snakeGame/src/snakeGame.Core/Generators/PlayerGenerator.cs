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
    public required Manager Manager { get; init; }

    private readonly Random _random = new();

    public IGenerate? Next { get; set; }

    public Result<bool> Generate()
    {
        GeneratePlayerHead();
        GeneratePlayerBody();

        if (Next != null)
        {
            return Next.Generate();
        }

        return new(true, null);
    }

    public void GeneratePlayerHead()
    {
        DynamicArray<Block> spaces = Manager.Spaces;
        Block[,] map = Manager.Map;

        Block player = UpdateSpaceBlockOut(spaces, _random.Next(0, spaces.Size));
        UpdateMapBlock(map, player.Cordinates, CharPlayerHead);

        Manager.Player = new(player);
    }

    private void GeneratePlayerBody()
    {
        (int y, int x) = Manager.Player.SeekRear().Cordinates;
        DynamicArray<Block> spaces = Manager.Spaces;
        Block[,] map = Manager.Map;

        Block playerBodyBlock;
        DirectionEnum direction = GetRandomDirection();
        for (int i = 0; i < PlayerInitialLength; i++)
        {
            (y, x, direction) = SelectValidCordinate(
            (y, x, direction),
            out int checkCordinateCount);
            if (checkCordinateCount == directionsLength)
            {
                break;
            }

            playerBodyBlock = UpdateSpaceBlockOut(spaces, GetBlockSearchFunction((y, x))!);
            UpdateMapBlock(map, (y, x), CharPlayerBody, direction);

            Manager.Player.InsertToRear(playerBodyBlock);
        }
    }

    private (int, int, DirectionEnum) SelectValidCordinate(
        (int, int, DirectionEnum) cordinates,
        out int checkCordinateCount)
    {
        Block[,] map = Manager.Map;
        (int, int) dimensions = Manager.Dimensions;
        (int y, int x, DirectionEnum direction) = cordinates;

        checkCordinateCount = 0;
        while (checkCordinateCount < directionsLength)
        {
            GetNextCordinate((y, x), direction, out int cordinateY, out int cordinateX);
            if (!IsValidCordinate(cordinateY, cordinateX, dimensions, map))
            {
                direction = GetNextDirection(direction);
                checkCordinateCount++;
                continue;
            }

            return (cordinateY, cordinateX, direction);
        }

        return default;
    }

    private static Func<Block, bool> GetBlockSearchFunction((int, int) cordinates)
    {
        return space => AreSameCordinates(space.Cordinates, cordinates);
    }
}