using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.DataStructures.NonLinear.HashMaps;
using pong.Core.State.Common;
using pong.Core.State.Game;
using static pong.Core.Shared.Errors;

namespace pong.Core.Validators;

public record GameModeValidator(
    HashMap<ArgumentTypeEnum, string?> ArgumentsMap,
    Arguments Arguments,
    Validator<HashMap<ArgumentTypeEnum, string?>, Arguments>? Next)
    : Validator<HashMap<ArgumentTypeEnum, string?>, Arguments>(ArgumentsMap, Arguments, Next)
{
    public override Result<Arguments> Validate()
    {
        if (!ArgumentsMap.TryGet(ArgumentTypeEnum.GameMode, out string? value))
        {
            return Next?.Validate() ?? new(Arguments);
        }

        if (!Enum.TryParse(value, out GameModeEnum gameMode) || !Enum.IsDefined(gameMode))
        {
            return new(null, ErrorInvalidGameMode());
        }

        Arguments.GameMode = gameMode;

        return Next?.Validate() ?? new(Arguments);
    }
}
