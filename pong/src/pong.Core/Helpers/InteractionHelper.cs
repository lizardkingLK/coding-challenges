using pong.Core.Abstractions;
using pong.Core.Builder;
using pong.Core.Inteactions;
using pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using pong.Core.State.Misc;

namespace pong.Core.Helpers;

public static class InteractionHelper
{
    public static IEnumerable<Interaction> GetGameInteractions(
        out GameBuilder gameBuilder,
        out Arguments arguments)
    {
        arguments = new();

        DynamicallyAllocatedArray<Interaction> interactions = new(
            new GameModeInteraction(arguments),
            new PlayerSideInteraction(arguments),
            new DifficultyInteraction(arguments),
            new PointsToWinInteraction(arguments));

        gameBuilder = new GameBuilder(arguments);

        return interactions.Values!;
    }

    public static IEnumerable<Interaction> GetMainMenuInteractions()
    {
        // TODO: implement main menu that appears after ending a level
        throw new NotImplementedException();
    }
    
    public static IEnumerable<Interaction> GetPauseMenuInteractions()
    {
        // TODO: implement pause menu that appears after ending a level
        throw new NotImplementedException();   
    }
}