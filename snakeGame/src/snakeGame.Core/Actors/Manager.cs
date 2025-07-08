using snakeGame.Core.Library;

namespace snakeGame.Core.Actors;

public class Manager
{
    public int Height { get; set; }

    public int Width { get; set; }

    public DynamicArray<Actor> Actors { get; } = new();

    public Actor EnemyActor { get; set; }

    public Library.LinkedList<Actor> PlayerActor { get; set; } = new();

    public Actor? GetActor(Func<Actor, bool> predicate, out int index)
    {
        index = -1;

        if (Actors.GetRandom(predicate, ref index, out Actor actor))
        {
            return actor;
        }

        return null;
    }
}
