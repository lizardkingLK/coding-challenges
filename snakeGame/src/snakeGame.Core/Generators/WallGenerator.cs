using snakeGame.Core.Abstractions;
using snakeGame.Core.Shared;
using snakeGame.Core.State;

using static snakeGame.Core.Shared.Constants;

using snakeGame.Core.Events;
using snakeGame.Core.Enums;

namespace snakeGame.Core.Generators;

public class WallGenerator : IGenerate
{
    public required Manager Manager { get; init; }

    public IGenerate? Next { get; set; }

    public Result<bool> Generate()
    {
        int height = Manager.Height;
        int width = Manager.Width;
        GameStatePublisher publisher = Manager.Publisher!;

        int i;
        int j;
        bool isSpaceTyped;
        for (i = 0; i < height; i++)
        {
            for (j = 0; j < width; j++)
            {
                isSpaceTyped = i > 0 && i < height - 1 && j > 0 && j < width - 1;
                if (isSpaceTyped)
                {
                    publisher.Publish(new GameState(
                        GameStateEnum.CreateSpace,
                        new()
                        {
                            CordinateY = i,
                            CordinateX = j,
                            Type = CharSpaceBlock,
                        }));
                    continue;
                }

                publisher.Publish(new GameState(
                    GameStateEnum.CreateWall,
                    new()
                    {
                        CordinateY = i,
                        CordinateX = j,
                        Type = CharWallBlock,
                    }));
            }
        }

        return Next?.Generate() ?? new(true, null);
    }
}