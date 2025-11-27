using tetris.Core.Attributes;
using tetris.Core.Enums.Misc;

namespace tetris.Core.Enums.Arguments;

[Argument(Prefix = "-pm", Name = "--play-mode", Type = ArgumentTypeEnum.PlayMode)]
public enum PlayModeEnum
{
    Drop,
    Float,   
}