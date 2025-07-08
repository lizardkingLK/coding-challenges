using snakeGame.Core.Abstractions;
using snakeGame.Core.Actors;
using snakeGame.Core.Enums;
using snakeGame.Core.Shared;

using static snakeGame.Core.Helpers.DirectionHelper;
using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Shared.Values;

namespace snakeGame.Core.Generators;

public class PlayerGenerator : IGenerate
{
    public IGenerate? Next { get; set; }

    public Result<bool> Generate(Manager manager)
    {
        CreatePlayer(manager);
        if (Next != null)
        {
            return Next.Generate(manager);
        }

        return new(true, null);
    }

    private static void CreatePlayer(Manager manager)
    {
        Actor? randomPlayerActor = manager.GetActor(
            actor => actor.State == CharSpaceBlock,
            out int index)
            ?? throw new Exception("error. actor value is null");

        Actor playerActor = randomPlayerActor.Value;
        playerActor.State = CharPlayerHead;

        manager.PlayerActor = CreatePlayerBody(
            new Tuple<int, int>(manager.Height, manager.Width),
            0,
            playerActor,
            new Library.LinkedList<Actor>()
            {
                Head = new(playerActor, null),
            });
    }

    private static Library.LinkedList<Actor> CreatePlayerBody(
        Tuple<int, int> sizes,
        int playerLength,
        Actor currentHead,
        Library.LinkedList<Actor> playerActor)
    {
        Console.WriteLine("HELLO DARKNESS MY OLD FRIEND");
        if (playerLength > PlayerInitialLength)
        {
            return playerActor;
        }

        (int currentCordinateY, int currentCordinateX) = currentHead.Position;
        int nextCordinateY;
        int nextCordinateX;
        Actor? newHead = null;
        foreach (DirectionEnum direction in directions)
        {
            (nextCordinateY, nextCordinateX) = GetNextCordinate(currentCordinateY, currentCordinateX, direction);
            if (!IsValidNonBlockingDirection(nextCordinateY, nextCordinateX, sizes.Item1, sizes.Item2))
            {
                continue;
            }

            newHead = new(new(nextCordinateY, nextCordinateX), null, CharPlayerBody);
            playerActor.InsertToEnd(newHead.Value);
            break;
        }

        return CreatePlayerBody(sizes, playerLength + 1, newHead!.Value, playerActor);
    }
}