namespace snakeGame.Core;

using snakeGame.Core.Actors;

using static Constants;
using static Values;
using static Utility;
using static Board;

public static class Helper
{
    

    public static Result<char[][]> InitializeGame(int height, int width)
    {
        // initialize board
        Result<Actor[][]> gameBoardResult = InitializeBoard(height, width);

        RenderBoard(gameBoardResult.Data!, height, width);

        // initialize Enemy


        // initialize player

        return new([], null);
    }
}