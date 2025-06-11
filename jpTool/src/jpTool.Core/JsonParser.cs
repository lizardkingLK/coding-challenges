namespace jpTool.Core;

using static Utility;
using static Validator;

public class JsonParser
{
    public static void ProcessValidation(string[] args)
    {
        DateTime startTime = DateTime.UtcNow;

        Result<bool> validArgumentsResult = IsValidArguments(args);
        if (validArgumentsResult.Errors != null)
        {
            Environment.ExitCode = 1;
            WriteError(validArgumentsResult.Errors);
            return;
        }

        Result<string> filePathResult = ProcessFilePath(args[0]);
        if (filePathResult.Errors != null)
        {
            Environment.ExitCode = 1;
            WriteError(filePathResult.Errors);
            return;
        }

        Result<bool> jsonValidateResult = ValidateJsonFile(filePathResult.Data!);
        if (jsonValidateResult.Errors != null)
        {
            Environment.ExitCode = 1;
            WriteError(jsonValidateResult.Errors);
            return;
        }

        WriteSuccess(startTime, DateTime.UtcNow);

        Environment.Exit(0);
    }
}
