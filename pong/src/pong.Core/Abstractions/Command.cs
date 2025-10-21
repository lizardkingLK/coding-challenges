using pong.Core.State.Game;

namespace pong.Core.Abstractions;

public abstract record Command(Arguments Arguments)
{
    public Arguments Arguments { get; set; } = Arguments;
    public abstract void Execute();
}