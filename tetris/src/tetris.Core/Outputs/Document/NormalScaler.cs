using tetris.Core.Enums.Arguments;
using tetris.Core.Shared;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Outputs.Document;

public class NormalScaler(MapSizeEnum mapSize) : DocumentOutput(mapSize)
{
    public override Result<bool> Validate(out int height, out int width)
    {
        height = HeightNormal;
        width = WidthNormal;

        return new(true);
    }
}