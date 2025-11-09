using tetris.Core.Attributes;

namespace tetris.Core.Enums;

[Argument(Prefix = "-gm", Name = "--game-mode", Type = ArgumentTypeEnum.GameMode)]
public enum GameModeEnum
{
    Classic,
    Timed,
    Points,
}