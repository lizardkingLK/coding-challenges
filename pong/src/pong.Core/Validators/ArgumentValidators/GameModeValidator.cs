using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.NonLinear.HashMaps;
using pong.Core.State;
using pong.Core.State.Game;
using static pong.Core.Shared.Errors;

namespace pong.Core.Validators.ArgumentValidators;

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
            return new(Arguments);
        }

        if (!Enum.TryParse(value, out GameModeEnum gameMode) || !Enum.IsDefined(gameMode))
        {
            return new(null, ErrorInvalidGameMode());
        }

        Arguments.GameMode = gameMode;

        if (Next == null)
        {
            return new(Arguments);
        }

        return Next.Validate();
    }
}
