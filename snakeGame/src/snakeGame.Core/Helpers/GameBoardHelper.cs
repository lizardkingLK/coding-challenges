using snakeGame.Core.Enums;
using snakeGame.Core.Library;
using snakeGame.Core.State;

namespace snakeGame.Core.Helpers;

public static class GameBoardHelper
{
    public static void UpdateSpaceBlockIn(DynamicArray<Block> spaces, Block block)
    {
        spaces.Add(block);
    }

    public static Block UpdateSpaceBlockOut(DynamicArray<Block> spaces, int index)
    {
        return spaces.Remove(index);
    }

    public static Block UpdateSpaceBlockOut(DynamicArray<Block> spaces, Func<Block, bool> searchFunction)
    {
        return spaces.Remove(searchFunction);
    }

    public static void UpdateMapBlock(Block[,] map, (int, int) cordinates, Block block)
    {
        map[cordinates.Item1, cordinates.Item2] = block;
    }

    public static void UpdateMapBlock(Block[,] map, (int, int) cordinates, char type)
    {
        map[cordinates.Item1, cordinates.Item2].Type = type;
    }

    public static void UpdateMapBlock(Block[,] map, (int, int) cordinates, DirectionEnum direction)
    {
        map[cordinates.Item1, cordinates.Item2].Direction = direction;
    }

    public static void UpdateMapBlock(Block[,] map, (int, int) cordinates, char type, DirectionEnum direction)
    {
        map[cordinates.Item1, cordinates.Item2].Type = type;
        map[cordinates.Item1, cordinates.Item2].Direction = direction;
    }
}