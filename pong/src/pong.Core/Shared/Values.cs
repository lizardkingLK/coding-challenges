using pong.Core.Enums;
using pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using pong.Core.Library.DataStructures.NonLinear.HashMaps;
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
        new("-d", Difficulty),
        new("--difficulty", Difficulty)
    );

    public static readonly DynamicallyAllocatedArray<string> unaryArguments = new(
        "-h", "--help", "-it", "--interactive"
    );
}