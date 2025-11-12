using tetris.Core.Abstractions;
using tetris.Core.State.Cordinates;
using static tetris.Core.Shared.Constants;

namespace tetris.Core.Streamers;

public record DocumentStreamer : IStreamer
{
    public void Clear()
    {
        Console.Clear();

        using FileStream fileStream = new(
            Path.Join(
                Directory.GetCurrentDirectory(),
                FileName),
                FileMode.Create);

        fileStream.Close();
    }

    public void Flush(in int height, in int width, in Block[,] map)
    {
        using FileStream fileStream = new(
            Path.Join(
                Directory.GetCurrentDirectory(),
                FileName),
                FileMode.Create);

        int length = width * height;
        int y;
        int x;
        int yPrevious = 0;
        for (int i = 0; i < length; i++)
        {
            y = i / width;
            x = i % width;
            if (yPrevious != y)
            {
                fileStream.WriteByte((byte)SymbolNewLine);
            }

            (_, char symbol, _) = map[y, x];
            fileStream.WriteByte((byte)symbol);

            yPrevious = y;
        }

        fileStream.Close();
    }

    public void Stream(in Block block, in int height, in int width, in Block[,] map)
    => Flush(height, width, map);
}