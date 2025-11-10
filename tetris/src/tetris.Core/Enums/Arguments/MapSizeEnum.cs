using tetris.Core.Attributes;
using tetris.Core.Enums.Misc;

namespace tetris.Core.Enums.Arguments;

[Argument(Prefix = "-ms", Name = "--map-size", Type = ArgumentTypeEnum.MapSize)]
public enum MapSizeEnum
{
    Normal,
    Scaled,
}