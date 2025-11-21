using tetris.Core.State.Cordinates;
using static tetris.Core.Helpers.BlockHelper;

namespace tetris.Core.Outputs.Console.Aligners;

public record CenterAligner : ConsoleAligner
{
    public override Position Root { get; set; }

    public override Position GetRoot(int height, int width)
    {
        return new(
            System.Console.WindowHeight / 2 - height / 2,
            System.Console.WindowWidth / 2 - width / 2);
    }

    public override void Align(ref Block block)
    {
        block = CreateBlock(Root + block.Position, block);
    }
}