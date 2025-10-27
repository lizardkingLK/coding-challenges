using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.DataStructures.NonLinear.HashMaps;
using pong.Core.State.Common;
using pong.Core.State.Game;

namespace pong.Core.Validators;

public record InteractiveValidator(
    HashMap<ArgumentTypeEnum, string?> ArgumentsMap,
    Arguments Arguments,
    Validator<HashMap<ArgumentTypeEnum, string?>, Arguments>? Next)
    : Validator<HashMap<ArgumentTypeEnum, string?>, Arguments>(ArgumentsMap, Arguments, Next)
{
    public override Result<Arguments> Validate()
    {
        if (ArgumentsMap.TryGet(ArgumentTypeEnum.Interactive, out _))
        {
            Arguments.CommandType = CommandTypeEnum.InteractiveCommand;
            return new(Arguments);
        }

        return Next?.Validate() ?? new(Arguments);
    }
}
