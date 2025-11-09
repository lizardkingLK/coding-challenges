using tetris.Core.Attributes;

namespace tetris.Core.Enums;

[Argument(Prefix = "-gm", Name = "--game-mode", Default = Classic, Type = ArgumentTypeEnum.GameMode)]
public enum GameModeEnum
{
    Timed,
    Classic,
    Points,
}