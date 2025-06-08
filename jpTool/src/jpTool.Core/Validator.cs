namespace jpTool.Core;

using static Constants;
using static Utility;

public static class Validator
{
    public static Result<bool> ValidateJsonFile(string filePath)
    {
        string fileContent = File.ReadAllText(filePath).Trim();
        if (string.IsNullOrEmpty(fileContent))
        {
            return new Result<bool>(false, ERROR_FILE_IS_EMPTY);
        }

        return ValidateJsonElement(fileContent);
    }

    public static Result<bool> ValidateJsonElement(string content)
    {
        string jsonString = ProcessRawJsonContent(content);
        if (string.IsNullOrEmpty(jsonString))
        {
            return new Result<bool>(false, ERROR_INVALID_JSON_FOUND);
        }

        int startIndex = 0;
        int endIndex = jsonString.Length - 1;
        if (jsonString[startIndex] == OpenCurlyBrace)
        {
            return ValidateJsonObject(jsonString, startIndex + 1, endIndex);
        }
        else if (jsonString[startIndex] == OpenSquareBrace)
        {
            return ValidateJsonArray(jsonString, startIndex + 1, endIndex);
        }

        return new Result<bool>(false, ERROR_INVALID_JSON_FOUND);
    }

    private static Result<(bool, int, char)> ValidateJsonKeyValue(string jsonString, int startIndex, int endIndex)
    {
        Result<(bool, int)> keyValidationResult = ValidateJsonKey(jsonString, startIndex, endIndex);
        if (keyValidationResult.Errors != null)
        {
            return new Result<(bool, int, char)>((false, -1, default), ERROR_INVALID_JSON_FOUND);
        }

        Result<(bool, int, char)> valueValidationResult = ValidateJsonValue(jsonString, keyValidationResult.Data.Item2, endIndex);
        if (valueValidationResult.Errors != null)
        {
            return new Result<(bool, int, char)>((false, -1, default), ERROR_INVALID_JSON_FOUND);
        }

        return valueValidationResult;
    }

    private static Result<(bool, int)> ValidateJsonKey(string jsonString, int startIndex, int endIndex)
    {
        int i;
        char previous;
        char current;
        char next;
        bool isValid = false;
        for (i = startIndex + 1; i < endIndex; i++)
        {
            previous = jsonString[i - 1];
            current = jsonString[i];
            next = jsonString[i + 1];
            if (previous != ReverseSolidus && current == Quotes)
            {
                isValid = next == Colon;
                break;
            }
        }

        if (isValid)
        {
            return new Result<(bool, int)>((true, i + 2), null);
        }

        return new Result<(bool, int)>((false, -1), ERROR_INVALID_JSON_FOUND);
    }

    private static Result<(bool, int, char)> ValidateJsonValue(string jsonString, int startIndex, int endIndex)
    {
        if (jsonString[startIndex] == Negative || (jsonString[startIndex] >= 48 && jsonString[startIndex] <= 57))
        {
            return ValidateJsonValueForNumber(jsonString, startIndex, endIndex);
        }
        else if (jsonString[startIndex] == Quotes)
        {
            return ValidateJsonValueForString(jsonString, startIndex, endIndex);
        }
        else if (jsonString[startIndex] == OpenCurlyBrace)
        {
            return ValidateJsonValueForObject(jsonString, startIndex, endIndex);
        }
        else if (jsonString[startIndex] == OpenSquareBrace)
        {
            return ValidateJsonValueForArray(jsonString, startIndex, endIndex);
        }

        Result<(bool, int, char)> result = ValidateJsonValueForNullOrBool(jsonString, startIndex, endIndex, Null);
        if (result.Data.Item1)
        {
            return result;
        }

        result = ValidateJsonValueForNullOrBool(jsonString, startIndex, endIndex, True);
        if (result.Data.Item1)
        {
            return result;
        }

        result = ValidateJsonValueForNullOrBool(jsonString, startIndex, endIndex, False);
        if (result.Data.Item1)
        {
            return result;
        }

        return result;
    }

    private static Result<(bool, int, char)> ValidateJsonValueForNumber(string jsonString, int startIndex, int endIndex)
    {
        int i;
        char next;
        bool isValid = false;
        char[] valueEndings = [Comma, CloseCurlyBrace, CloseSquareBrace];
        for (i = startIndex + 1; i < endIndex; i++)
        {
            next = jsonString[i + 1];
            if (valueEndings.Contains(next))
            {
                isValid = IsValidNumber(jsonString[startIndex..(i + 1)]);
                break;
            }
        }

        if (isValid)
        {
            return new Result<(bool, int, char)>((true, i + 1, jsonString[i + 1]), null);
        }

        return new Result<(bool, int, char)>((false, -1, default), ERROR_INVALID_JSON_FOUND);
    }

    private static Result<(bool, int, char)> ValidateJsonValueForString(string jsonString, int startIndex, int endIndex)
    {
        int i;
        char previous;
        char current;
        char next;
        bool isValid = false;
        char[] valueEndings = [Comma, CloseCurlyBrace, CloseSquareBrace];
        for (i = startIndex + 1; i < endIndex; i++)
        {
            previous = jsonString[i - 1];
            current = jsonString[i];
            next = jsonString[i + 1];
            if (previous != ReverseSolidus && current == Quotes)
            {
                isValid = valueEndings.Contains(next);
                break;
            }
        }

        if (isValid)
        {
            return new Result<(bool, int, char)>((true, i + 1, jsonString[i + 1]), null);
        }

        return new Result<(bool, int, char)>((false, -1, default), ERROR_INVALID_JSON_FOUND);
    }

    private static Result<(bool, int, char)> ValidateJsonValueForArray(string jsonString, int startIndex, int endIndex)
    {
        int i;
        char current;
        char next;
        bool isValid = false;
        char[] valueEndings = [Comma, CloseSquareBrace];
        Result<(bool, int, char)> arrayValueValidationResult;
        for (i = startIndex + 1; i < endIndex; i++)
        {
            current = jsonString[i];
            next = jsonString[i + 1];
            if (current == CloseSquareBrace)
            {
                isValid = valueEndings.Contains(next);
                break;
            }

            arrayValueValidationResult = ValidateJsonValue(jsonString, i, endIndex);
            i = arrayValueValidationResult.Data.Item2;

            if (!arrayValueValidationResult.Data.Item1)
            {
                isValid = false;
                break;
            }

            if (arrayValueValidationResult.Data.Item3 == CloseSquareBrace)
            {
                isValid = true;
                break;
            }
        }

        if (!isValid)
        {
            return new Result<(bool, int, char)>((false, -1, default), ERROR_INVALID_JSON_FOUND);
        }

        if (i == endIndex)
        {
            return new Result<(bool, int, char)>((true, endIndex, jsonString[endIndex]), null);
        }

        return new Result<(bool, int, char)>((true, i + 1, jsonString[i + 1]), null);
    }

    private static Result<(bool, int, char)> ValidateJsonValueForObject(string jsonString, int startIndex, int endIndex)
    {
        int i;
        char current;
        char next;
        bool isValid = false;
        char[] valueEndings = [Comma, CloseCurlyBrace];
        Result<(bool, int, char)> objectValueValidationResult;
        for (i = startIndex + 1; i < endIndex; i++)
        {
            current = jsonString[i];
            next = jsonString[i + 1];
            if (current == CloseCurlyBrace)
            {
                isValid = valueEndings.Contains(next);
                break;
            }

            objectValueValidationResult = ValidateJsonKeyValue(jsonString, i, endIndex);
            i = objectValueValidationResult.Data.Item2;

            if (!objectValueValidationResult.Data.Item1)
            {
                isValid = false;
                break;
            }

            if (objectValueValidationResult.Data.Item3 == CloseCurlyBrace)
            {
                isValid = true;
                break;
            }
        }

        if (!isValid)
        {
            return new Result<(bool, int, char)>((false, -1, default), ERROR_INVALID_JSON_FOUND);
        }

        if (i == endIndex)
        {
            return new Result<(bool, int, char)>((true, endIndex, jsonString[endIndex]), null);
        }

        return new Result<(bool, int, char)>((true, i + 1, jsonString[i + 1]), null);
    }

    private static Result<(bool, int, char)> ValidateJsonValueForNullOrBool(string jsonString, int startIndex, int endIndex, string literal)
    {
        int length = literal.Length;
        int iLiteral = 0;
        int i = 0;
        while (iLiteral < length && i < length)
        {
            if (literal[i] != jsonString[startIndex + i])
            {
                return new Result<(bool, int, char)>((false, -1, default), ERROR_INVALID_JSON_FOUND);
            }

            iLiteral++;
            i++;
        }

        if (startIndex + i > endIndex)
        {
            return new Result<(bool, int, char)>((false, -1, default), ERROR_INVALID_JSON_FOUND);
        }

        startIndex += i;
        char[] valueEndings = [Comma, CloseCurlyBrace, CloseSquareBrace];
        if (!valueEndings.Contains(jsonString[startIndex]))
        {
            return new Result<(bool, int, char)>((false, -1, default), ERROR_INVALID_JSON_FOUND);
        }

        return new Result<(bool, int, char)>((true, startIndex, jsonString[startIndex]), null);
    }

    private static Result<bool> ValidateJsonObject(string jsonString, int startIndex, int endIndex)
    {
        if (jsonString[startIndex..endIndex] == string.Empty)
        {
            return new Result<bool>(true, null);
        }

        Result<(bool, int, char)>? valueValidationResult;

        bool isValid;
        while (true)
        {
            valueValidationResult = ValidateJsonKeyValue(jsonString, startIndex, endIndex);
            startIndex = valueValidationResult.Data.Item2 + 1;
            if (valueValidationResult.Errors != null)
            {
                return new Result<bool>(false, ERROR_INVALID_JSON_FOUND);
            }

            if (valueValidationResult.Data.Item3 == CloseCurlyBrace)
            {
                isValid = valueValidationResult.Data.Item1;
                break;
            }

            if (startIndex >= endIndex)
            {
                return new Result<bool>(false, ERROR_INVALID_JSON_FOUND);
            }
        }

        if (isValid)
        {
            return new Result<bool>(true, null);
        }

        return new Result<bool>(false, ERROR_INVALID_JSON_FOUND);
    }

    private static Result<bool> ValidateJsonArray(string jsonString, int startIndex, int endIndex)
    {
        if (jsonString[startIndex..endIndex] == string.Empty)
        {
            return new Result<bool>(true, null);
        }

        Result<(bool, int, char)>? valueValidationResult;

        bool isValid;
        while (true)
        {
            valueValidationResult = ValidateJsonValue(jsonString, startIndex, endIndex);
            startIndex = valueValidationResult.Data.Item2 + 1;
            if (valueValidationResult.Errors != null)
            {
                return new Result<bool>(false, ERROR_INVALID_JSON_FOUND);
            }

            if (valueValidationResult.Data.Item3 == CloseSquareBrace)
            {
                isValid = valueValidationResult.Data.Item1;
                break;
            }

            if (startIndex >= endIndex)
            {
                return new Result<bool>(false, ERROR_INVALID_JSON_FOUND);
            }
        }

        if (isValid)
        {
            return new Result<bool>(true, null);
        }

        return new Result<bool>(false, ERROR_INVALID_JSON_FOUND);
    }
}
