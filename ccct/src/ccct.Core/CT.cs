using ccct.Core.Library.NonLinear.HashMaps;
using ccct.Core.State.Common;
using ccct.Core.State.Console;
using static ccct.Core.Helpers.ApplicationHelper;
using static ccct.Core.Helpers.ArgumentsHelper;
using static ccct.Core.Helpers.CompressionHelper;

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

        Result<HashMap<char, long>> countsResult = TrackFrequency(argumentResult.Data.InputFile);
        if (countsResult.HasErrors)
        {
            HandleError(countsResult.Errors);
        }

        OutputResult(countsResult.Data);

        return string.Empty;
    }
}
