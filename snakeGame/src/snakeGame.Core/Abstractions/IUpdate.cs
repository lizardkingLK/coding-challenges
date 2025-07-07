using snakeGame.Core.Actors;

namespace snakeGame.Core.Abstractions;

public interface IUpdate
{
    public void Update(int height, int width, Actor[][] actors);
}