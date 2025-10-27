using pong.Core.Abstractions;
using pong.Core.State.Game;
using pong.Core.State.Handlers;
using static pong.Core.Shared.Constants;

namespace pong.Core.Outputs.Document;

public record DocumentOutput : Output
{
    public DocumentOutput(GameManager GameManager) : base(GameManager)
    {
        (Height, Width) = (DefaultHeight, DefaultWidth);

        if (File.Exists(DocumentOutputName))
        {
            File.Delete(DocumentOutputName);
        }
    }

    public override void Draw(Block block)
    {
        (int top, int left, char symbol, _) = block;
        using FileStream fileStream = new(Path.Join(Directory.GetCurrentDirectory(), DocumentOutputName), FileMode.Create);

        fileStream.Position = (top * Width) + left;
        // System.Console.WriteLine(fileStream.Position);
        // byte b = (byte)symbol;
        fileStream.WriteByte((byte)symbol);
    }
}