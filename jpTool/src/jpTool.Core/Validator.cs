namespace jpTool.Core
{
    using static Constants;

    public static class Validator
    {
        public static Result<bool> ValidateJson(string filePath)
        {
            string jsonString = File.ReadAllText(filePath).Trim();
            if (string.IsNullOrEmpty(jsonString))
            {
                return new Result<bool>(false, ERROR_FILE_IS_EMPTY);
            }

            if (!jsonString.StartsWith('{') || !jsonString.EndsWith('}'))
            {
                return new Result<bool>(false, ERROR_INVALID_JSON_FOUND);
            }

            return new Result<bool>(true, null);
        }
    }
}