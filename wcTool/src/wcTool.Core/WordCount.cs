
namespace wcTool.Core;

public class WordCount
{
    private const string ResponseFormat = " {0} {1}";

    public static Result<string> GetFileNameAndBytes(string filePath)
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

    public static Result<string> GetFileNameAndLineCount(string filePath)
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
}
