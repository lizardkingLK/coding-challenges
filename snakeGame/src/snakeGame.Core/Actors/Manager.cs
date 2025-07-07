using snakeGame.Core.Library;

using static snakeGame.Core.Shared.Constants;

namespace snakeGame.Core.Actors;

public class Manager
{
    private readonly Random random = new();

    public int Height { get; set; }

    public int Width { get; set; }

    public required Actor[][] Actors { get; set; }

    public HashMap<string, Actor> SpaceTracker { get; set; } = new();

    public Actor EnemyActor { get; set; }

    public Actor PlayerActor { get; set; }

    internal void CreateEnemy()
    {
        string randomKey = GetRandomKey(out int yCordinate, out int xCordinate);
        Actor enemyActor = SpaceTracker.Remove(randomKey);

        enemyActor.State = CharEnemy;
        enemyActor.ForegroundColor = EnemyColor;
        EnemyActor = enemyActor;

        Actors[yCordinate][xCordinate] = enemyActor;
    }

    internal void CreatePlayer()
    {
        string randomKey = GetRandomKey(out int yCordinate, out int xCordinate);
        Actor playerActor = SpaceTracker.Remove(randomKey);

        playerActor.State = CharPlayerHead;
        playerActor.ForegroundColor = PlayerColor;
        PlayerActor = playerActor;

        Actors[yCordinate][xCordinate] = playerActor;
    }

    private string GetRandomKey(out int yCordinate, out int xCordinate)
    {
        yCordinate = random.Next(1, Height - 1);
        xCordinate = random.Next(1, Width - 1);

        return $"{yCordinate}_{xCordinate}";
    }
}