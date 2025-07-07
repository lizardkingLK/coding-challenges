namespace snakeGame.Core.Library;

public struct HashNode<K, V>(K key, V value)
{
    public readonly K Key { get; } = key;

    public V Value { get; set; } = value;
}