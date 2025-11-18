using tetris.Core.Enums.Arguments;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;
using static tetris.Core.Helpers.BlockHelper;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Outputs.Console;

public class DoubleScaler(MapSizeEnum mapSize) : ConsoleOutput(mapSize)
{
    public override void Flush()
    {
        int length = HeightNormal * WidthNormal;
        int y;
        int x;
        Block block;
        for (int i = 0; i < length; i++)
        {
            y = i / WidthNormal;
            x = i % WidthNormal;
            block = Map![y, x];
            Stream(block);
        }
    }

    public override void Stream(Block block)
    {
        foreach (Block transformed in CreateScaledBlock(Root, block))
        {
            Streamer.Stream(transformed, HeightScaled, WidthScaled, Map!);
        }
    }

    public override Result<bool> Validate(out int height, out int width)
    {
        height = HeightScaled;
        width = WidthScaled;

        int availableHeight = System.Console.WindowHeight;
        if (availableHeight < height)
        {
            return new(false, "error. cannot create map. height not enough");
        }

        int availableWidth = System.Console.WindowWidth;
        if (availableWidth < width)
        {
            return new(false, "error. cannot create map. width not enough");
        }

        return new(true);
    }
}