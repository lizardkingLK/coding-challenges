namespace jpTool.Core;

using static Utility;
using static Validator;
using static Constants;

public class JsonParser
{
    public static void ProcessValidation(string[] args)
    {
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

        Result<bool> jsonValidateResult = ValidateJson(filePathResult.Data!);
        if (jsonValidateResult.Errors != null)
        {
            Environment.ExitCode = 1;
            WriteError(jsonValidateResult.Errors);
            return;
        }

        WriteSuccess(SUCCESS_VALID_JSON_FOUND);
        Environment.Exit(0);
    }
}
