using snakeGame.Core.Abstractions;
using snakeGame.Core.State;

namespace snakeGame.Core.Output.Web;

public class WebOutput : IOutput
{
    public Manager? Manager { get; set; }

    public void Output(GameState? state = null)
    {
        if (state == null)
        {
            return;
        }

        if (state.Value.Data!.HasValue)
        {
            System.Console.WriteLine(state.Value.Type);
            System.Console.WriteLine("\ty = {0}, x = {1}", state.Value.Data.Value.CordinateY, state.Value.Data.Value.CordinateX);
        }
        else
        {
            System.Console.WriteLine(state.Value.Type);
        }

    }

    public void Stream(GameState state)
    {
        Output(state);
    }
}