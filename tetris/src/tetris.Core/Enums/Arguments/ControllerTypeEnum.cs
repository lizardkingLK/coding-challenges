using tetris.Core.Attributes;
using tetris.Core.Enums.Misc;

namespace tetris.Core.Enums.Arguments;

[Argument(Prefix = "-h", Name = "--help", Type = ArgumentTypeEnum.Help, IsSwitch = true)]
[Argument(Prefix = "-it", Name = "--interactive", Type = ArgumentTypeEnum.Interactive, IsSwitch = true)]
[Argument(Prefix = "-s", Name = "--scores", Type = ArgumentTypeEnum.Scores, IsSwitch = true)]
public enum ControllerTypeEnum
{
    Game,
    Help,
    Scores,
    Interactive,
}
