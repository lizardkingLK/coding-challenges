namespace wcTool.Core;

public class WordCount
{
    private const string ResponseFormat = " {0} {1}";
    private static readonly char[] wordSeparator = new char[] { ' ', '\n', '\t', '\r' };

    public static Result<string> CountBytes(string filePath)
    {
        Result<string> processedPathResult = Utility.GetProcessedFilePath(filePath);
        if (processedPathResult.Error != null)
        {
            return processedPathResult;
        }

        filePath = processedPathResult.Data!;
        byte[] bytes = File.ReadAllBytes(filePath);
        long byteCount = bytes.LongLength;
        string fileName = Path.GetFileName(filePath);

        return new Result<string>(
            string.Format(ResponseFormat, byteCount, fileName),
            null);
    }

    public static Result<string> CountLines(string filePath)
    {
        Result<string> processedPathResult = Utility.GetProcessedFilePath(filePath);
        if (processedPathResult.Error != null)
        {
            return processedPathResult;
        }

        filePath = processedPathResult.Data!;
        string[] lines = File.ReadAllLines(filePath);
        long lineCount = lines.LongLength;
        string fileName = Path.GetFileName(filePath);

        return new Result<string>(
            string.Format(ResponseFormat, lineCount, fileName),
            null);
    }

    public static Result<string> CountWords(string filePath)
    {
        Result<string> processedPathResult = Utility.GetProcessedFilePath(filePath);
        if (processedPathResult.Error != null)
        {
            return processedPathResult;
        }

        filePath = processedPathResult.Data!;

        long wordCount = 0;
        using (StreamReader streamReader = new(File.OpenRead(filePath)))
        {
            if (!streamReader.BaseStream.CanRead)
            {
                return new Result<string>(null, Errors.FILE_IN_USE);
            }

            var content = streamReader.ReadToEnd().Split(wordSeparator, StringSplitOptions.RemoveEmptyEntries);

            wordCount = content.LongLength;
        }

        string fileName = Path.GetFileName(filePath);

        return new Result<string>(
            string.Format(ResponseFormat, wordCount, fileName),
            null);
    }

    public static Result<string> CountCharacters(string filePath)
    {
        Result<string> processedPathResult = Utility.GetProcessedFilePath(filePath);
        if (processedPathResult.Error != null)
        {
            return processedPathResult;
        }

        filePath = processedPathResult.Data!;

        long charCount = 0;
        using (StreamReader streamReader = new(File.OpenRead(filePath)))
        {
            if (!streamReader.BaseStream.CanRead)
            {
                return new Result<string>(null, Errors.FILE_IN_USE);
            }

            while (streamReader.Read() != -1)
            {
                charCount++;
            }
        }

        string fileName = Path.GetFileName(filePath);

        return new Result<string>(
            string.Format(ResponseFormat, charCount, fileName),
            null);
    }
}
