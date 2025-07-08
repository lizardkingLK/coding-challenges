namespace snakeGame.Core.Library;

public class LinkNode<T>(T value, LinkNode<T>? next = null)
{
    public T Value { get; } = value;
    public LinkNode<T>? Next { get; set; } = next;
}