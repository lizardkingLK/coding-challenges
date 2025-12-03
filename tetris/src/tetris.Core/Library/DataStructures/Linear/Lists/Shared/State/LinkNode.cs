namespace tetris.Core.Library.DataStructures.Linear.Lists.Shared.State;

public record LinkNode<T>(
    LinkNode<T>? Previous,
    T Value,
    LinkNode<T>? Next)
{
    public LinkNode(T value) : this(null, value, null) { }

    public LinkNode<T>? Previous { get; set; } = Previous;
    public T Value { get; set; } = Value;
    public LinkNode<T>? Next { get; set; } = Next;
}