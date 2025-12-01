using tetris.Core.State.Misc;

namespace tetris.Core.Abstractions;

public interface IInteraction
{
    public Arguments? Arguments { get; set; }
    
    public void Display();
    public void Prompt();
    public void Process();
}