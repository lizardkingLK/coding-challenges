using pong.Core.State.Game;
using pong.Core.State.Handlers;

namespace pong.Core.Abstractions;

public abstract record Output(GameManager GameManager)
{
    public int Height { get; set; }
    public int Width { get; set; }
    public abstract void Draw(Block block);
}