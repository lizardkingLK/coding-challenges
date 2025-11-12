using tetris.Core.Attributes;
using tetris.Core.Enums.Misc;

namespace tetris.Core.Enums.Arguments;

[Argument(Prefix = "-gm", Name = "--game-mode", Type = ArgumentTypeEnum.GameMode)]
public enum GameModeEnum
{
    Classic,
    Timed,
    Points,
}