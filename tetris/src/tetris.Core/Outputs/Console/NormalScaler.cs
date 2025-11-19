using tetris.Core.Enums.Arguments;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;
using static tetris.Core.Helpers.BlockHelper;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Outputs.Console;

public class NormalScaler(MapSizeEnum mapSize) : ConsoleOutput(mapSize)
{
    public override void Flush()
    {
        Block[,] centered = new Block[HeightNormal, WidthNormal];
        int length = HeightNormal * WidthNormal;
        int y;
        int x;
        Block block;
        for (int i = 0; i < length; i++)
        {
            y = i / WidthNormal;
            x = i % WidthNormal;
            block = Map![y, x];
            centered[y, x] = CreateBlock(
                Root + block.Position,
                block.Symbol,
                block.Color);
        }

        Streamer.Flush(HeightNormal, WidthNormal, centered);
    }

    public override void Stream(Block block)
    {
        Streamer.Stream(
            CreateBlock(Root + block.Position, block),
            HeightNormal,
            WidthNormal,
            Map!);
    }

    public override Result<bool> Validate()
    {
        Height = HeightNormal;
        Width = WidthNormal;

        int availableHeight = System.Console.WindowHeight;
        if (availableHeight < Height)
        {
            return new(false, "error. cannot create map. Height not enough");
        }

        int availableWidth = System.Console.WindowWidth;
        if (availableWidth < Width)
        {
            return new(false, "error. cannot create map. Width not enough");
        }

        return new(true);
    }
}