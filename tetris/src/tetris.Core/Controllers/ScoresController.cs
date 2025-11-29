using System.Text;
using tetris.Core.Abstractions;
using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.Shared;
using tetris.Core.State.Misc;
using static tetris.Core.Helpers.ConsoleHelper;
using static tetris.Core.Helpers.ScoresHelper;
using static tetris.Core.Shared.Constants;
using static tetris.Core.Shared.Values;

namespace tetris.Core.Controllers;

public class ScoresController : IController
{
    public Result<bool> Execute()
    {
        Result<DynamicallyAllocatedArray<GameScore>> scoresResult = Select();
        if (scoresResult.Errors != null)
        {
            return new(false, scoresResult.Errors);
        }

        StringBuilder scoresBuilder = new();
        int totalWidth = Console.WindowWidth;
        int headerSize = scoreHeaders.Size;
        WriteHead(scoresBuilder, totalWidth, headerSize);
        // WriteBody(scoresResult.Data!, scoresBuilder, totalWidth);

        return new(true);
    }

    private void WriteBody(
        DynamicallyAllocatedArray<GameScore> scores,
        StringBuilder scoresBuilder,
        int totalWidth)
    {
        int size = scores.Size;
        // foreach
        // WriteContent
    }

    private static void WriteHead(
        StringBuilder scoresBuilder,
        int totalWidth,
        int headerSize)
    {
        WriteSeparator(scoresBuilder, totalWidth, headerSize);
        WriteHeader(scoresBuilder, totalWidth, headerSize);
        WriteSeparator(scoresBuilder, totalWidth, headerSize);
    }

    private static void WriteHeader(
        StringBuilder scoresBuilder,
        int totalWidth,
        int headerSize)
    {
        int width = totalWidth / headerSize;
        for (int i = 0; i < headerSize; i++)
        {
            scoresBuilder.Append(SymbolPipe);
            WriteWithChars(scoresBuilder, scoreHeaders[i]!, width - 1, SymbolSpaceBlock);
        }

        scoresBuilder
        .Remove(scoresBuilder.Length - 1, 1)
        .Append(SymbolPipe);

        WriteAndClear(scoresBuilder);
    }

    private static void WriteSeparator(StringBuilder scoresBuilder, int totalWidth, int headerSize)
    {
        int width = totalWidth / headerSize;
        for (int i = 0; i < headerSize; i++)
        {
            scoresBuilder.Append(SymbolPlus);
            WriteWithChars(scoresBuilder, string.Empty, width - 1, SymbolMinus);
        }

        scoresBuilder
        .Remove(scoresBuilder.Length - 1, 1)
        .Append(SymbolPlus);

        WriteAndClear(scoresBuilder);
    }

    private static void WriteWithChars(
        StringBuilder scoresBuilder,
        string scoreHeader,
        int width,
        char symbol)
    {
        scoresBuilder.Append(scoreHeader);
        int length = width - scoreHeader.Length;
        for (int i = 0; i < length; i++)
        {
            scoresBuilder.Append(symbol);
        }
    }

    private static void WriteAndClear(StringBuilder scoresBuilder)
    {
        Write(scoresBuilder.ToString(), ConsoleColor.Green);
        _ = scoresBuilder.Clear();
    }
}