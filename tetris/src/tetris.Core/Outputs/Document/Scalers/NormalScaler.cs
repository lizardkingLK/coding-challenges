using tetris.Core.Enums.Arguments;
using tetris.Core.Gamplay;
using tetris.Core.Shared;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Outputs.Document.Scalers;

public class NormalScaler(MapSizeEnum mapSize) : DocumentGameplay(mapSize)
{
    public override Result<bool> Validate()
    {
        Height = HeightNormal;
        Width = WidthNormal;

        return new(true);
    }
}