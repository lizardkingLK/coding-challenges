namespace ccct.Core.Helpers;

public class FileHelper
{
    public static IEnumerable<char> ReadAllText(string inputFile)
    {
        using FileStream fileStream = File.OpenRead(inputFile);

        int read;
        while ((read = fileStream.ReadByte()) != -1)
        {
            yield return (char)read;
        }
    }
}