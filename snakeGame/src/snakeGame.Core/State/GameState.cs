using snakeGame.Core.Enums;

namespace snakeGame.Core.State;

public record struct GameState(GameStateEnum Type, Block? Data, int? Height = null, int? Width = null);