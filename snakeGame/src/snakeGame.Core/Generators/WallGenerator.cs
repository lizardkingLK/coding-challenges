using snakeGame.Core.Abstractions;
using snakeGame.Core.Library;
using snakeGame.Core.Shared;
using snakeGame.Core.State;

using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Helpers.GameBoardHelper;

namespace snakeGame.Core.Generators;

public class WallGenerator : IGenerate
{
    public required Manager Manager { get; init; }

    public IGenerate? Next { get; set; }

    public Result<bool> Generate()
    {
        int height = Manager.Height;
        int width = Manager.Width;
        Block[,] map = Manager.Map;
        DynamicArray<Block> spaces = Manager.Spaces;

        int i;
        int j;
        bool isSpaceTyped;
        Block currentBlock;
        for (i = 0; i < height; i++)
        {
            for (j = 0; j < width; j++)
            {
                isSpaceTyped = i > 0 && i < height - 1 && j > 0 && j < width - 1;
                if (!isSpaceTyped)
                {
                    currentBlock = new()
                    {
                        CordinateY = i,
                        CordinateX = j,
                        Type = CharWallBlock,
                    };
                    UpdateMapBlock(map, (i, j), currentBlock);
                    continue;
                }

                currentBlock = new()
                {
                    CordinateY = i,
                    CordinateX = j,
                    Type = CharSpaceBlock,
                };
                UpdateMapBlock(map, (i, j), currentBlock);
                UpdateSpaceBlockIn(spaces, currentBlock);
            }
        }

        if (Next != null)
        {
            return Next.Generate();
        }

        return new(true, null);
    }
}