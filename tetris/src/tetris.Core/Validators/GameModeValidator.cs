using tetris.Core.Abstractions;
using tetris.Core.Enums.Arguments;
using tetris.Core.Enums.Misc;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps;
using tetris.Core.Shared;
using tetris.Core.State.Misc;

namespace tetris.Core.Validators;

public class GameModeValidator(
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
        if (!Values.TryGetValue(ArgumentTypeEnum.GameMode, out string? input))
        {
            return Next?.Validate() ?? new(Value);
        }

        if (!Enum.TryParse(input, out GameModeEnum value) || !Enum.IsDefined(value))
        {
            return new(null, $"error. invalid game mode value provided: {input}");
        }

        Value.GameMode = value;

        return Next?.Validate() ?? new(Value);
    }

    public static bool TryValidate(
        string? value,
        out GameModeEnum gameMode)
        => Enum.TryParse(value, out gameMode)
        && Enum.IsDefined(gameMode);
}