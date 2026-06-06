using System.Collections;
using System.Diagnostics.CodeAnalysis;
using ccct.Core.Library.Linear.Arrays;
using ccct.Core.Library.Linear.Lists;
using static ccct.Core.Shared.Errors;

namespace ccct.Core.Library.NonLinear.HashMaps;

public class HashMap<K, V> : IEnumerable<(K, V?)> where K : notnull
{
    private record HashNode(K Key, V? Value)
    {
        public V? Value { get; set; } = Value;

        public static implicit operator (K, V?)(HashNode hashNode)
        {
            return (hashNode.Key, hashNode.Value);
        }
    };

    private DynamicallyAllocatedArray<DoublyLinkedList<HashNode>> _buckets;

    private const float LOAD_FACTOR = .7f;
    private const int INITIAL_SIZE = 2;

    private readonly float _loadFactor;

    public int Size { get; private set; }

    public int Capacity { get; private set; }

    public V? this[K key]
    {
        get => Search(key);
        set => Update(key, value);
    }

    public HashMap(int capacity = INITIAL_SIZE)
    {
        if (capacity < 0 || capacity >= Array.MaxLength)
        {
            throw new ApplicationException(ErrorHashMapInvalidCapacity);
        }

        if (capacity == 0)
        {
            capacity = INITIAL_SIZE;
        }

        Capacity = capacity;
        _loadFactor = LOAD_FACTOR;
        _buckets = new(capacity);
    }

    public void Add(K key, V value)
    {
        if (ContainsKey(key, out DoublyLinkedList<HashNode>? bucket, out _))
        {
            throw new ApplicationException(ErrorHashMapKeyAlreadyExist);
        }

        bucket!.AddToRear(new(key, value));
        Size++;
        RehashIfSatisfies();
    }

    private void RehashIfSatisfies()
    {
        if ((float)Size / Capacity <= _loadFactor)
        {
            return;
        }

        int newCapacity = Capacity * 2;
        DynamicallyAllocatedArray<DoublyLinkedList<HashNode>> newBuckets = new(newCapacity);
        DoublyLinkedList<HashNode>? newBucket;
        foreach (DoublyLinkedList<HashNode> bucket in _buckets)
        {
            foreach ((K key, V? value) in bucket)
            {
                int newIndex = GetIndex(key, newCapacity);
                newBucket = newBuckets[newIndex];
                if (newBucket == null)
                {
                    newBucket = new();
                    _buckets[newIndex] = newBucket;
                }

                newBucket.AddToRear(new(key, value));
            }
        }

        _buckets = newBuckets;
        Capacity = newCapacity;
    }

    public IEnumerator<(K, V?)> GetEnumerator()
    {
        foreach (DoublyLinkedList<HashNode> bucket in _buckets)
        {
            foreach ((K, V?) node in bucket)
            {
                yield return node;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private V? Search(K key)
    {
        if (!ContainsKey(key, out _, out HashNode? hashNode))
        {
            throw new ApplicationException(ErrorHashMapKeyNotFound);
        }

        return hashNode.Value;
    }

    private void Update(K key, V? value)
    {
        if (!ContainsKey(key, out DoublyLinkedList<HashNode>? bucket, out _))
        {
            throw new ApplicationException(ErrorHashMapKeyNotFound);
        }

        bucket!.Update(
            item => item != null && item.Key.Equals(key),
            new(key, value));
    }

    private void Remove(K key)
    {
        if (!ContainsKey(key, out DoublyLinkedList<HashNode>? bucket, out _))
        {
            throw new ApplicationException(ErrorHashMapKeyNotFound);
        }

        bucket!.Remove(
            item => item != null && item.Key.Equals(key));
    }

    private bool ContainsKey(
        K key,
        out DoublyLinkedList<HashNode>? bucket,
        [NotNullWhen(true)] out HashNode? value)
    {
        value = null;

        int index = GetIndex(key, Capacity);
        if (_buckets[index] == null)
        {
            bucket = new();
            _buckets[index] = bucket;
            return false;
        }

        bucket = _buckets[index];

        bool IsNonNullValue(HashNode hashNode) =>
        hashNode.Key != null && hashNode.Key.Equals(key);

        return bucket.TryGetValue(IsNonNullValue, out value);
    }

    private static int GetIndex(K key, int capacity)
    {
        return HashMap<K, V?>.GetAbsolute(key.GetHashCode()) % capacity;
    }

    private static int GetAbsolute(int value)
    {
        int complement = value >> 31;
        return (value ^ complement) - complement;
    }
}