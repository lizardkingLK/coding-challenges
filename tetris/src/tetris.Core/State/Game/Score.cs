using tetris.Core.Enums.Game;

namespace tetris.Core.State.Game;

public record Score
{
    public int Value { get; set; }
    public GameStateEnum GameState { get; set; }
}