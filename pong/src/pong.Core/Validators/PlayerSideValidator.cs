using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.DataStructures.NonLinear.HashMaps;
using pong.Core.State.Common;
using pong.Core.State.Misc;
using static pong.Core.Shared.Errors;

namespace pong.Core.Validators;

public record PlayerSideValidator(
    HashMap<ArgumentTypeEnum, string?> ArgumentsMap,
    Arguments Arguments,
    Validator<HashMap<ArgumentTypeEnum, string?>, Arguments>? Next)
    : Validator<HashMap<ArgumentTypeEnum, string?>, Arguments>(ArgumentsMap, Arguments, Next)
{
    public override Result<Arguments> Validate()
    {
        if (!ArgumentsMap.TryGet(ArgumentTypeEnum.PlayerSide, out string? value))
        {
            return Next?.Validate() ?? new(Arguments);
        }

        if (!Enum.TryParse(value, out PlayerSideEnum playerSide) || !Enum.IsDefined(playerSide))
        {
            return new(null, ErrorInvalidPlayerSide());
        }

        Arguments.PlayerSide = playerSide;

        return Next?.Validate() ?? new(Arguments);
    }
}
