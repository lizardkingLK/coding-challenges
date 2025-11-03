using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.DataStructures.NonLinear.HashMaps;
using pong.Core.State.Common;
using pong.Core.State.Misc;
using static pong.Core.Shared.Errors;

namespace pong.Core.Validators;

public record PointsToWinValidator(
    HashMap<ArgumentTypeEnum, string?> ArgumentsMap,
    Arguments Arguments,
    Validator<HashMap<ArgumentTypeEnum, string?>, Arguments>? Next)
    : Validator<HashMap<ArgumentTypeEnum, string?>, Arguments>(ArgumentsMap, Arguments, Next)
{
    public override Result<Arguments> Validate()
    {
        if (!ArgumentsMap.TryGet(ArgumentTypeEnum.PointsToWin, out string? value))
        {
            return Next?.Validate() ?? new(Arguments);
        }

        if (!int.TryParse(value, out int pointsToWin) || pointsToWin <= 0)
        {
            return new(null, ErrorInvalidPointsToWin());
        }

        Arguments.PointsToWin = pointsToWin;

        return Next?.Validate() ?? new(Arguments);
    }
}