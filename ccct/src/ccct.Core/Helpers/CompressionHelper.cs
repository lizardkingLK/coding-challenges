using ccct.Core.Library.NonLinear.HashMaps;
using ccct.Core.State.Common;
using static ccct.Core.Helpers.FileHelper;

namespace ccct.Core.Helpers;

public class CompressionHelper
{
    public static Result<HashMap<char, long>> TrackFrequency(string inputFile)
    {
        HashMap<char, long> values = [];
        foreach (char character in ReadAllText(inputFile))
        {
            if (!values.TryAdd(character, 1))
            {
                values[character]++;
            }
        }

        return new(values);
    }

    public static void OutputResult(HashMap<char, long> data)
    {
        using StreamWriter streamWriter = new(
            Path.Combine(
                AppContext.BaseDirectory,
                @"../../../../../output.txt"));

        foreach ((char, long) item in data)
        {
            streamWriter.WriteLine("(" + "'" + item.Item1 + "', " + item.Item2 + "),");
        }

        streamWriter.Flush();
    }
}