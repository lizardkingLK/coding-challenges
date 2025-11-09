using tetris.Core.Attributes;

namespace tetris.Core.Enums;

[Argument(Prefix = "-h", Name = "--help", Type = ArgumentTypeEnum.Help, IsSwitch = true)]
[Argument(Prefix = "-it", Name = "--interactive", Type = ArgumentTypeEnum.Interactive, IsSwitch = true)]
public enum ControllerTypeEnum
{
    Help,
    Game,
    Interactive,
}
