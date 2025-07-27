namespace snakeGame.Core.Library;

public record HashNode<K, V>
{
    public K Key { get; }

    public V Value { get; set; }

    public HashNode(K key, V value)
    {
        Key = key;
        Value = value;
    }
}