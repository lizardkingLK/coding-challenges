using tetris.Core.Attributes;
using tetris.Core.Enums.Misc;

namespace tetris.Core.Enums.Arguments;

[Argument(Prefix = "-o", Name = "--output", Type = ArgumentTypeEnum.OutputType)]
public enum OutputTypeEnum
{
    Console,
    Document,
}