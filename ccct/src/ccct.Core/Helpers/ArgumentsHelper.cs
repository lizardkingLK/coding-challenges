using ccct.Core.State.Common;
using ccct.Core.State.Console;

namespace ccct.Core.Helpers;

public class ArgumentsHelper
{
    public static Result<Arguments> ValidateArguments(string[] argumentsArray)
    {
        if (argumentsArray.Length == 0 || !Path.Exists(argumentsArray[0]))
        {
            return new(null, "error. required content path was not given");
        }

        return new(new()
        {
            InputFile = argumentsArray[0],
        });
    }
}