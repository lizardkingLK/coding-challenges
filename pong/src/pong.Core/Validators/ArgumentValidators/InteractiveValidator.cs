using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.NonLinear.HashMaps;
using pong.Core.State;
using pong.Core.State.Game;

namespace pong.Core.Validators.ArgumentValidators;

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

        if (Next == null)
        {
            return new(Arguments);
        }

        return Next.Validate();
    }
}
