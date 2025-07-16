namespace snakeGame.Core.Library;

public struct HashNode<K, V>(K key, V value)
{
    public readonly K Key { get; } = key;

    public V Value { get; set; } = value;

    public readonly void Deconstruct(out K key, out V value)
    {
        key = Key;
        value = Value;
    }
}