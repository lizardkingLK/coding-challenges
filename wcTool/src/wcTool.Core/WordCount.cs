namespace wcTool.Core;

public class WordCount
{
    private const string FormatGetFileNameAndBytes = "{0} {1}";

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
            string.Format(FormatGetFileNameAndBytes, byteCount, fileName),
            null);
    }
}
