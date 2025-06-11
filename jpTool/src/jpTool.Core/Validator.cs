namespace jpTool.Core;

using static Constants;
using static Values;
using static Error;
using static Utility;

public static class Validator
{
    private static char[] jsonString = [];

    private static int endIndex = -1;

    public static Result<bool> ValidateJsonFile(string filePath)
    {
        jsonString = File.ReadAllText(filePath).ToCharArray();
        if (jsonString.Length == 0)
        {
            return ErrorFileIsEmpty;
        }

        int startIndex = 0;
        endIndex = jsonString.Length - 1;
        if (!ValidateNextCharacter(ref startIndex, [OpenCurlyBrace, OpenSquareBrace]))
        {
            return ErrorInvalidJsonFound;
        }

        if (jsonString[startIndex] == OpenCurlyBrace)
        {
            return ValidateJsonObject(startIndex + 1);
        }

        if (jsonString[startIndex] == OpenSquareBrace)
        {
            return ValidateJsonArray(startIndex + 1);
        }

        return ErrorInvalidJsonFound;
    }

    private static bool ValidateNextCharacter(ref int startIndex, char[] nextCharacters)
    {
        char current;
        while (startIndex <= endIndex)
        {
            current = jsonString[startIndex];
            if (IsValueIncluded(nextCharacters, current))
            {
                return true;
            }

            if (!IsValueIncluded(whitespaceCharacters, current))
            {
                return false;
            }

            startIndex++;
        }

        return false;
    }

    private static int ValidateFirstOccuringCharacter(ref int startIndex, char[] firstCharacterSet, char secondCharacter)
    {
        char current;
        while (startIndex <= endIndex)
        {
            current = jsonString[startIndex];
            if (IsValueIncluded(firstCharacterSet, current))
            {
                return 0;
            }

            if (current == secondCharacter)
            {
                return 1;
            }

            if (!IsValueIncluded(whitespaceCharacters, current))
            {
                return -1;
            }

            startIndex++;
        }

        return -1;
    }

    private static Result<bool> ValidateLegalTrailing(int startIndex)
    {
        char current;
        while (startIndex <= endIndex)
        {
            current = jsonString[startIndex];
            if (!IsValueIncluded(whitespaceCharacters, current))
            {
                return ErrorInvalidJsonFound;
            }

            startIndex++;
        }

        return new Result<bool>(true, null);
    }

    private static Result<bool> ValidateJsonObject(int startIndex)
    {
        int firstOccurringCharacter = ValidateFirstOccuringCharacter(ref startIndex, [Quotes], CloseCurlyBrace);
        if (firstOccurringCharacter == 1)
        {
            return ValidateLegalTrailing(startIndex + 1);
        }

        if (firstOccurringCharacter == -1)
        {
            return ErrorInvalidJsonFound;
        }

        Result<(bool, int)>? valueValidationResult;
        while (true)
        {
            valueValidationResult = ValidateJsonKeyValue(startIndex);
            startIndex = valueValidationResult.Data.Item2;
            if (valueValidationResult.Errors != null)
            {
                return ErrorInvalidJsonFound;
            }

            firstOccurringCharacter = ValidateFirstOccuringCharacter(ref startIndex, [Comma], CloseCurlyBrace);
            if (firstOccurringCharacter == 1)
            {
                return ValidateLegalTrailing(startIndex + 1);
            }

            if (firstOccurringCharacter == -1)
            {
                return ErrorInvalidJsonFound;
            }

            startIndex++;
            if (!ValidateNextCharacter(ref startIndex, [Quotes]))
            {
                return ErrorInvalidJsonFound;
            }
        }
    }

    private static Result<(bool, int)> ValidateJsonKeyValue(int startIndex)
    {
        Result<(bool, int)> keyValidationResult = ValidateJsonKey(startIndex);
        if (keyValidationResult.Errors != null)
        {
            return ErrorInvalidJsonFoundWithIndex;
        }

        startIndex = keyValidationResult.Data.Item2;
        if (!ValidateNextCharacter(ref startIndex, [Colon]))
        {
            return ErrorInvalidJsonFoundWithIndex;
        }

        startIndex++;
        if (!ValidateNextCharacter(ref startIndex, valuePrefixes))
        {
            return ErrorInvalidJsonFoundWithIndex;
        }

        return ValidateJsonValue(startIndex);
    }

    private static Result<(bool, int)> ValidateJsonKey(int startIndex)
    {
        int i;
        char current;
        bool isValid = false;

        bool isInEscapeSequence = false;
        int escapeStartIndex = -1;
        for (i = startIndex + 1; i < endIndex; i++)
        {
            current = jsonString[i];
            if (current == FullLineBreak || current == FullTabspace)
            {
                isValid = false;
                break;
            }

            if (current == Backslash && !isInEscapeSequence)
            {
                isInEscapeSequence = true;
                escapeStartIndex = i;
            }

            if (current == Quotes && !isInEscapeSequence)
            {
                isValid = !isInEscapeSequence;
                break;
            }

            if (!isInEscapeSequence)
            {
                continue;
            }

            if (i != escapeStartIndex && IsValidEscapeSequence(jsonString[escapeStartIndex..(i + 1)]))
            {
                isInEscapeSequence = false;
            }
        }

        if (isValid)
        {
            return new Result<(bool, int)>((true, i + 1), null);
        }

        return ErrorInvalidJsonFoundWithIndex;
    }

    private static Result<(bool, int)> ValidateJsonValue(int startIndex)
    {
        char valuePrefix = jsonString[startIndex];
        if (valuePrefix == Negative || (valuePrefix >= 48 && valuePrefix <= 57))
        {
            return ValidateJsonValueForNumber(startIndex);
        }

        if (valuePrefix == Quotes)
        {
            return ValidateJsonValueForString(startIndex);
        }

        if (valuePrefix == OpenCurlyBrace)
        {
            return ValidateJsonValueForObject(startIndex);
        }

        if (valuePrefix == OpenSquareBrace)
        {
            return ValidateJsonValueForArray(startIndex);
        }

        if (valuePrefix == CharNullStart)
        {
            return ValidateJsonValueForNullOrBool(startIndex, Null);
        }

        if (valuePrefix == CharTrueStart)
        {
            return ValidateJsonValueForNullOrBool(startIndex, True);
        }

        if (valuePrefix == CharFalseStart)
        {
            return ValidateJsonValueForNullOrBool(startIndex, False);
        }

        return ErrorInvalidJsonFoundWithIndex;
    }

    private static Result<bool> ValidateJsonArray(int startIndex)
    {
        int firstOccurringCharacter = ValidateFirstOccuringCharacter(ref startIndex, valuePrefixes, CloseSquareBrace);
        if (firstOccurringCharacter == 1)
        {
            return ValidateLegalTrailing(startIndex + 1);
        }

        if (firstOccurringCharacter == -1)
        {
            return ErrorInvalidJsonFound;
        }

        Result<(bool, int)> valueValidationResult;
        while (true)
        {
            valueValidationResult = ValidateJsonValue(startIndex);
            startIndex = valueValidationResult.Data.Item2;
            if (valueValidationResult.Errors != null)
            {
                return ErrorInvalidJsonFound;
            }

            firstOccurringCharacter = ValidateFirstOccuringCharacter(ref startIndex, [Comma], CloseSquareBrace);
            if (firstOccurringCharacter == 1)
            {
                return ValidateLegalTrailing(startIndex + 1);
            }

            if (firstOccurringCharacter == -1)
            {
                return ErrorInvalidJsonFound;
            }

            startIndex++;
            if (!ValidateNextCharacter(ref startIndex, valuePrefixes))
            {
                return ErrorInvalidJsonFound;
            }
        }
    }

    private static Result<(bool, int)> ValidateJsonValueForNumber(int startIndex)
    {
        int i;
        char next;
        bool isValid = false;
        for (i = startIndex; i < endIndex; i++)
        {
            next = jsonString[i + 1];
            if (IsValueIncluded(numberValueEndings, next))
            {
                isValid = IsValidNumber(jsonString[startIndex..(i + 1)]);
                break;
            }
        }

        if (isValid)
        {
            return new Result<(bool, int)>((true, i + 1), null);
        }

        return ErrorInvalidJsonFoundWithIndex;
    }

    private static Result<(bool, int)> ValidateJsonValueForString(int startIndex)
    {
        int i;
        char current;
        bool isValid = false;

        bool isInEscapeSequence = false;
        int escapeStartIndex = -1;
        for (i = startIndex + 1; i < endIndex; i++)
        {
            current = jsonString[i];
            if (current == FullLineBreak || current == FullTabspace)
            {
                isValid = false;
                break;
            }

            if (current == Backslash && !isInEscapeSequence)
            {
                isInEscapeSequence = true;
                escapeStartIndex = i;
            }

            if (current == Quotes && !isInEscapeSequence)
            {
                isValid = true;
                break;
            }

            if (!isInEscapeSequence)
            {
                continue;
            }

            if (i != escapeStartIndex && IsValidEscapeSequence(jsonString[escapeStartIndex..(i + 1)]))
            {
                isInEscapeSequence = false;
            }
        }

        if (isValid)
        {
            return new Result<(bool, int)>((true, i + 1), null);
        }

        return ErrorInvalidJsonFoundWithIndex;
    }

    private static Result<(bool, int)> ValidateJsonValueForArray(int startIndex)
    {
        startIndex++;
        int firstOccurringCharacter = ValidateFirstOccuringCharacter(ref startIndex, valuePrefixes, CloseSquareBrace);
        if (firstOccurringCharacter == 1)
        {
            return new Result<(bool, int)>((true, startIndex + 1), null);
        }

        if (firstOccurringCharacter == -1)
        {
            return ErrorInvalidJsonFoundWithIndex;
        }

        Result<(bool, int)> arrayValueValidationResult;
        while (startIndex <= endIndex)
        {
            arrayValueValidationResult = ValidateJsonValue(startIndex);
            startIndex = arrayValueValidationResult.Data.Item2;
            if (!arrayValueValidationResult.Data.Item1)
            {
                return ErrorInvalidJsonFoundWithIndex;
            }

            firstOccurringCharacter = ValidateFirstOccuringCharacter(ref startIndex, [Comma], CloseSquareBrace);
            if (firstOccurringCharacter == 1)
            {
                return new Result<(bool, int)>((true, startIndex + 1), null);
            }

            if (firstOccurringCharacter == -1)
            {
                return ErrorInvalidJsonFoundWithIndex;
            }

            startIndex++;
            if (!ValidateNextCharacter(ref startIndex, valuePrefixes))
            {
                return ErrorInvalidJsonFoundWithIndex;
            }
        }

        return ErrorInvalidJsonFoundWithIndex;
    }

    private static Result<(bool, int)> ValidateJsonValueForObject(int startIndex)
    {
        startIndex++;
        int firstOccurringCharacter = ValidateFirstOccuringCharacter(ref startIndex, [Quotes], CloseCurlyBrace);
        if (firstOccurringCharacter == 1)
        {
            return new Result<(bool, int)>((true, startIndex + 1), null);
        }

        if (firstOccurringCharacter == -1)
        {
            return ErrorInvalidJsonFoundWithIndex;
        }

        Result<(bool, int)> objectValueValidationResult;
        while (startIndex <= endIndex)
        {
            objectValueValidationResult = ValidateJsonKeyValue(startIndex);
            startIndex = objectValueValidationResult.Data.Item2;
            if (!objectValueValidationResult.Data.Item1)
            {
                return ErrorInvalidJsonFoundWithIndex;
            }

            firstOccurringCharacter = ValidateFirstOccuringCharacter(ref startIndex, [Comma], CloseCurlyBrace);
            if (firstOccurringCharacter == 1)
            {
                return new Result<(bool, int)>((true, startIndex + 1), null);
            }

            if (firstOccurringCharacter == -1)
            {
                return ErrorInvalidJsonFoundWithIndex;
            }

            startIndex++;
            if (!ValidateNextCharacter(ref startIndex, [Quotes]))
            {
                return ErrorInvalidJsonFoundWithIndex;
            }
        }

        return ErrorInvalidJsonFoundWithIndex;
    }

    private static Result<(bool, int)> ValidateJsonValueForNullOrBool(int startIndex, string literal)
    {
        int length = literal.Length;
        int iLiteral = 0;
        int i = 0;
        while (iLiteral < length && i < length)
        {
            if (literal[i] != jsonString[startIndex + i])
            {
                return ErrorInvalidJsonFoundWithIndex;
            }

            iLiteral++;
            i++;
        }

        return new Result<(bool, int)>((true, startIndex + i), null);
    }
}
