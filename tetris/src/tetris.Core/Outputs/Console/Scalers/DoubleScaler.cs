using tetris.Core.Enums.Arguments;
using tetris.Core.Gamplay;
using tetris.Core.Shared;
using tetris.Core.State.Cordinates;
using static tetris.Core.Helpers.BlockHelper;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Outputs.Console.Scalers;

public class DoubleScaler(MapSizeEnum mapSize) : ConsoleGameplay(mapSize)
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
            Output.Stream(transformed, HeightScaled, WidthScaled, Map!);
        }
    }

    public override Result<bool> Validate()
    {
        Height = HeightScaled;
        Width = WidthScaled;

        int availableHeight = System.Console.WindowHeight;
        if (availableHeight < Height)
        {
            return new(false, "error. cannot create map. height not enough");
        }

        int availableWidth = System.Console.WindowWidth;
        if (availableWidth < Width)
        {
            return new(false, "error. cannot create map. width not enough");
        }

        return new(true);
    }
}