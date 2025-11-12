using tetris.Core.Attributes;
using tetris.Core.Enums.Misc;

namespace tetris.Core.Enums.Arguments;

[Argument(Prefix = "-h", Name = "--help", Type = ArgumentTypeEnum.Help, IsSwitch = true)]
[Argument(Prefix = "-it", Name = "--interactive", Type = ArgumentTypeEnum.Interactive, IsSwitch = true)]
public enum ControllerTypeEnum
{
    Game,
    Help,
    Interactive,
}
