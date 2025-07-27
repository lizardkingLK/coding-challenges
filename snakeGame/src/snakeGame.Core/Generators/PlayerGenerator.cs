using snakeGame.Core.Abstractions;
using snakeGame.Core.Shared;
using snakeGame.Core.State;
using snakeGame.Core.Enums;

using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Helpers.DirectionHelper;
using snakeGame.Core.Events;

namespace snakeGame.Core.Generators;

public class PlayerGenerator : IGenerate
{
    public required Manager Manager { get; init; }

    public IGenerate? Next { get; set; }

    public Result<bool> Generate()
    {
        GeneratePlayerHead();
        GeneratePlayerBody();

        return Next?.Generate() ?? new(true, null);
    }

    public void GeneratePlayerHead()
    {
        Manager.Publisher!.Publish(new(GameStateEnum.CreatePlayerHead, null));
    }

    private void GeneratePlayerBody()
    {
        (int y, int x) = Manager.Player.SeekRear().Cordinates;
        GameStatePublisher publisher = Manager.Publisher!;

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

            publisher.Publish(new(
                GameStateEnum.CreatePlayerBody, new()
                {
                    CordinateY = y,
                    CordinateX = x,
                    Direction = direction,
                }));                        
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
}