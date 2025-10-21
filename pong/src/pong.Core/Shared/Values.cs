using pong.Core.Enums;
using pong.Core.Library.NonLinear.HashMaps;
using static pong.Core.Enums.ArgumentTypeEnum;

namespace pong.Core.Shared;

public static class Values
{
    public static readonly HashMap<string, ArgumentTypeEnum> allArgumentsMap = new(
        new("-h", Help),
        new("--help", Help),
        new("-it", Interactive),
        new("--interactive", Interactive),
        new("-gm", GameMode),
        new("--game-mode", GameMode),
        new("-o", OutputType),
        new("--output", OutputType),
        new("-d", Difficulty),
        new("--difficulty", Difficulty)
    );
}