namespace pong.Core.Abstractions;

public abstract record Interaction
{
    public virtual bool Validate() => true;

    public abstract void Display();
    public abstract void Prompt();
    public abstract void Process();
}