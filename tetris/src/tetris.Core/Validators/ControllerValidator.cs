using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Enums.Misc;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Shared;
using tetris.Core.State.Misc;

namespace tetris.Core.Validators;

public class ControllerValidator(
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
        ControllerTypeEnum? controllerType = null;
        if (Values.TryGetValue(ArgumentTypeEnum.Help, out _))
        {
            controllerType = ControllerTypeEnum.Help;
        }
        else if (Values.TryGetValue(ArgumentTypeEnum.Scores, out _))
        {
            controllerType = ControllerTypeEnum.Scores;
        }
        else if (Values.TryGetValue(ArgumentTypeEnum.Interactive, out _))
        {
            controllerType = ControllerTypeEnum.Interactive;
        }

        if (controllerType != null)
        {
            Value.ControllerType = controllerType.Value;
            return new(Value);
        }

        return Next?.Validate() ?? new(Value);
    }
}