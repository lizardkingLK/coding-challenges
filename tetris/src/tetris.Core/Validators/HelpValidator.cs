using tetris.Core.Abstractions;
using tetris.Core.Enums;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Shared;
using tetris.Core.State;

namespace tetris.Core.Validators;

public class HelpValidator(
    Arguments Value,
    HashMap<ArgumentTypeEnum, string> Values,
    IValidator<ArgumentTypeEnum, Arguments>? Next = null)
: IValidator<ArgumentTypeEnum, Arguments>
{
    public Arguments Value { get; init; } = Value;

    public HashMap<ArgumentTypeEnum, string> Values { get; init; } = Values;

    public IValidator<ArgumentTypeEnum, Arguments>? Next { get; init; } = Next;

    public Result<Arguments> Validate()
    {
        if (Values.TryGetValue(ArgumentTypeEnum.Help, out _))
        {
            Value.ControllerType = ControllerTypeEnum.Help;
            return new(Value);
        }

        return Next?.Validate() ?? new(Value);
    }
}