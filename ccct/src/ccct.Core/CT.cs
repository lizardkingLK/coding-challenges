using ccct.Core.State.Common;
using ccct.Core.State.Console;
using static ccct.Core.Helpers.ArgumentsHelper;
using static ccct.Core.Helpers.ApplicationHelper;

namespace ccct.Core;

public static class CT
{
    public static string Compress(string[] arguments)
    {
        Result<Arguments> argumentResult = ValidateArguments(arguments);
        if (argumentResult.HasErrors)
        {
            HandleError(argumentResult.Errors);
        }



        return "";
    }
}
