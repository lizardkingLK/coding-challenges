using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Enums.Misc;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Shared;
using tetris.Core.State.Misc;

namespace tetris.Core.Validators;

public class OutputTypeValidator(
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
        if (!Values.TryGetValue(ArgumentTypeEnum.OutputType, out string? input))
        {
            return Next?.Validate() ?? new(Value);
        }

        if (!Enum.TryParse(input, out OutputTypeEnum value) || !Enum.IsDefined(value))
        {
            return new(null, $"error. invalid output type value provided: {input}");
        }

        Value.OutputType = value;

        return Next?.Validate() ?? new(Value);
    }
}