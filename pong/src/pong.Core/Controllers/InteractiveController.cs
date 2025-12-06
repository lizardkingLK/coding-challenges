using pong.Core.Abstractions;
using pong.Core.Builder;
using pong.Core.State.Misc;
using static pong.Core.Helpers.InteractionHelper;

namespace pong.Core.Controllers;

public record InteractiveController(Arguments Arguments) : Controller(Arguments)
{
    public override void Execute()
    {
        Create(out GameBuilder gameBuilder, out Arguments arguments);

        new GameController(arguments, gameBuilder).Execute();
    }

    public static void Create(out GameBuilder gameBuilder, out Arguments arguments)
    {
        IEnumerable<Interaction> interactions =
        GetGameInteractions(out gameBuilder, out arguments);
        foreach (Interaction? interaction in interactions)
        {
            if (interaction == null || !interaction.Validate())
            {
                continue;
            }

            interaction.Display();
            interaction.Prompt();
            interaction.Process();
        }
    }
}