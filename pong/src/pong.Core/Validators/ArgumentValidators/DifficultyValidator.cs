using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.NonLinear.HashMaps;
using pong.Core.State;
using pong.Core.State.Game;
using static pong.Core.Shared.Errors;

namespace pong.Core.Validators.ArgumentValidators;

public record DifficultyValidator(
    HashMap<ArgumentTypeEnum, string?> ArgumentsMap,
    Arguments Arguments,
    Validator<HashMap<ArgumentTypeEnum, string?>, Arguments>? Next)
    : Validator<HashMap<ArgumentTypeEnum, string?>, Arguments>(ArgumentsMap, Arguments, Next)
{
    public override Result<Arguments> Validate()
    {
        if (!ArgumentsMap.TryGet(ArgumentTypeEnum.Difficulty, out string? value))
        {
            return new(Arguments);
        }

        if (!Enum.TryParse(value, out DifficultyLevelEnum difficultyType) || !Enum.IsDefined(difficultyType))
        {
            return new(null, ErrorInvalidDifficulty());
        }

        Arguments.DifficultyLevel = difficultyType;

        if (Next == null)
        {
            return new(Arguments);
        }

        return Next.Validate();
    }
}