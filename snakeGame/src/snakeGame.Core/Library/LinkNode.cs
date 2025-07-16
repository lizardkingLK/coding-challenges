namespace snakeGame.Core.Library;

public class LinkNode<T>(T value, LinkNode<T>? next = null, LinkNode<T>? previous = null)
{
    public T Value { get; } = value;
    public LinkNode<T>? Next { get; set; } = next;
    public LinkNode<T>? Previous { get; set; } = previous;
}