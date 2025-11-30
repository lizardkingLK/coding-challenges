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
    private const string noScoresMessage = "NO SCORES SO FAR";

    public Result<bool> Execute()
    {
        Result<DynamicallyAllocatedArray<DynamicallyAllocatedArray<string>>> scoresResult = Select();
        if (scoresResult.Errors != null)
        {
            return new(false, scoresResult.Errors);
        }

        StringBuilder scoresBuilder = new();
        int totalWidth = Console.WindowWidth;
        int headerSize = scoreHeaders.Size;

        ClearConsole();

        WriteHead(scoresBuilder, totalWidth, headerSize);
        WriteBody(scoresResult.Data!, scoresBuilder, totalWidth, headerSize);

        return new(true);
    }

    private static void WriteBody(
        DynamicallyAllocatedArray<DynamicallyAllocatedArray<string>> scores,
        StringBuilder scoresBuilder,
        int totalWidth,
        int headerSize)
    {
        int size = scores.Size;
        if (size == 0)
        {
            WriteAt(SymbolPipe, 3, 0, ColorInfo);
            WriteAt(noScoresMessage, 3, totalWidth / 2 - noScoresMessage.Length / 2, ConsoleColor.Yellow);
            WriteAt(SymbolPipe, 3, totalWidth - 1, ColorInfo);
            WriteSeparator(scoresBuilder, totalWidth, headerSize);
            return;
        }

        for (int i = 0; i < size; i++)
        {
            WriteScore(scores[i]!, scoresBuilder, totalWidth, headerSize);
        }

        WriteSeparator(scoresBuilder, totalWidth, headerSize);
    }

    private static void WriteScore(
        DynamicallyAllocatedArray<string> gameScore,
        StringBuilder scoresBuilder,
        int totalWidth,
        int headerSize)
    {
        int size = gameScore.Size;
        for (int i = 0; i < size; i++)
        {
            scoresBuilder.Append(SymbolPipe);
            WriteWithChars(
               scoresBuilder,
               gameScore[i]!,
               (totalWidth / headerSize) - 1,
               SymbolSpaceBlock);
        }

        scoresBuilder
        .Remove(scoresBuilder.Length - 1, 1)
        .Append(SymbolPipe);

        WriteAndClear(scoresBuilder);
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
            WriteWithChars(
                scoresBuilder,
                scoreHeaders[i]!,
                width - 1,
                SymbolSpaceBlock);
        }

        scoresBuilder
        .Remove(scoresBuilder.Length - 1, 1)
        .Append(SymbolPipe);

        WriteAndClear(scoresBuilder);
    }

    private static void WriteSeparator(
        StringBuilder scoresBuilder,
        int totalWidth,
        int headerSize)
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
        string content,
        int width,
        char symbol)
    {
        scoresBuilder.Append(content);
        int length = width - content.Length;
        for (int i = 0; i < length; i++)
        {
            scoresBuilder.Append(symbol);
        }
    }

    private static void WriteAndClear(StringBuilder scoresBuilder)
    {
        Write(scoresBuilder.ToString(), ColorInfo);
        _ = scoresBuilder.Clear();
    }
}