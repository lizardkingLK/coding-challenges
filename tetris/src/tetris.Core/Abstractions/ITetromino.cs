using tetris.Core.Enums.Cordinates;

namespace tetris.Core.Abstractions;

public interface ITetromino
{
    public bool[,] Get();
    public void Rotate();
    public void Move(DirectionEnum direction);
}