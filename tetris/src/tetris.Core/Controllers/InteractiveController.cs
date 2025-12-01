using tetris.Core.Abstractions;
using tetris.Core.Interactions;
using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.Shared;
using tetris.Core.State.Misc;

namespace tetris.Core.Controllers;

public class InteractiveController : IController
{
    private readonly Arguments _arguments = new();
    private readonly DynamicallyAllocatedArray<IInteraction> _interactions
    = new(
        new DifficultyInteraction(),
        new GameModeInteraction(),
        new MapSizeInteraction(),
        new PlayModeInteraction());

    public Result<bool> Execute()
    {
        foreach (IInteraction? interaction in _interactions.Values)
        {
            interaction!.Arguments = _arguments;

            interaction.Display();
            interaction.Prompt();
            interaction.Process();
        }

        return new GameController(_arguments).Execute();
    }
}
