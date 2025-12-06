using System.Collections;
using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using tetris.Core.Library.DataStructures.Linear.Lists;
using tetris.Core.Library.DataStructures.NonLinear.HashMaps.State;
using static tetris.Core.Library.DataStructures.NonLinear.HashMaps.Shared.Constants;

namespace tetris.Core.Library.DataStructures.NonLinear.HashMaps;

public class HashMap<K, V> : IEnumerable<V?> where K : notnull
{
    private DynamicallyAllocatedArray<DoublyLinkedList<HashNode<K, V>>> _buckets = [];

    private readonly float _loadFactor = GrowthFactor;

    public int Capacity { get; private set; } = InitialCapacity;

    public int Size { get; private set; }

    public V this[K key]
    {
        get => Get(key);
        set => Update(key, value);
    }

    public HashMap(params (K key, V? value)[] values)
    {
        AddRange(values);
    }

    public void Add(K key, V value)
    {
        if (ContainsKey(key, out DoublyLinkedList<HashNode<K, V>>? bucket, out _))
        {
            throw new Exception("error. cannot add value. key already contain");
        }

        bucket!.AddToTail(new(key, value));
        Size++;

        ReHashIfSatisfies();
    }

    public bool TryAdd(K key, V value)
    {
        if (ContainsKey(key, out DoublyLinkedList<HashNode<K, V>>? bucket, out _))
        {
            return false;
        }

        bucket!.AddToTail(new(key, value));
        Size++;

        ReHashIfSatisfies();

        return true;
    }

    public void AddRange(params (K key, V? value)[] values)
    {
        foreach ((K key, V? value) in values)
        {
            Add(key, value!);
        }
    }

    public void Update(K key, V value)
    {
        if (!ContainsKey(key, out _, out HashNode<K, V>? hashNode))
        {
            throw new Exception("error. cannot update value. key does not contain");
        }

        hashNode!.Value = value;
    }

    public bool TryUpdate(K key, V value)
    {
        if (!ContainsKey(key, out _, out HashNode<K, V>? hashNode))
        {
            return false;
        }

        hashNode!.Value = value;

        return true;
    }

    public bool TryAddOrUpdate(K key, V value)
    {
        if (ContainsKey(key, out _, out HashNode<K, V>? hashNode))
        {
            hashNode!.Value = value;
        }
        else
        {
            Add(key, value);
        }

        return true;
    }

    public IEnumerable<HashNode<K, V>> GetHashNodes()
    {
        foreach (DoublyLinkedList<HashNode<K, V>>? bucket in _buckets.Values)
        {
            if (bucket is null)
            {
                continue;
            }

            foreach (HashNode<K, V> hashNode in bucket.ValuesHeadToTail)
            {
                yield return hashNode;
            }
        }
    }

    public IEnumerable<KeyValuePair<K, V>> GetKeyValues()
    {
        foreach ((K key, V value) in GetHashNodes())
        {
            yield return new(key, value);
        }
    }

    public V Get(K key)
    {
        if (!ContainsKey(key, out _, out HashNode<K, V>? hashNode))
        {
            throw new Exception("error. cannot get value. key does not contain");
        }

        return hashNode!.Value;
    }

    public bool TryGetValue(K key, out V? value)
    {
        value = default;

        if (!ContainsKey(key, out _, out HashNode<K, V>? hashNode))
        {
            return false;
        }

        value = hashNode!.Value;

        return true;
    }

    public V Remove(K key)
    {
        if (!ContainsKey(key, out DoublyLinkedList<HashNode<K, V>>? bucket, out HashNode<K, V>? hashNode))
        {
            throw new Exception("error. cannot remove value. key does not contain");
        }

        bucket!.Remove(hashNode!);

        return hashNode!.Value;
    }

    public bool TryRemove(K key, out V? value)
    {
        value = default;

        if (!ContainsKey(key, out DoublyLinkedList<HashNode<K, V>>? bucket, out HashNode<K, V>? hashNode))
        {
            return false;
        }

        bucket!.Remove(hashNode!);

        value = hashNode!.Value;

        return true;
    }

    public IEnumerator<V?> GetEnumerator()
    {
        foreach ((_, V? value) in GetKeyValues())
        {
            yield return value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private bool ContainsKey(
        K key,
        out DoublyLinkedList<HashNode<K, V>>? bucket,
        out HashNode<K, V>? value)
    {
        value = default;

        int index = HashMap<K, V>.GetBucketIndex(key, Capacity);
        bool doesBucketContain = _buckets.TryGetValue(index, out bucket);
        bool containsKey = false;
        if (doesBucketContain)
        {
            containsKey = bucket!.TryGetValue(value => value.Key!.Equals(key), out value);
        }
        else
        {
            bucket = _buckets.Add(new(), index);
        }

        return containsKey;
    }

    private void ReHashIfSatisfies()
    {
        if ((float)Size / Capacity < _loadFactor)
        {
            return;
        }

        Capacity *= 2;
        DynamicallyAllocatedArray<DoublyLinkedList<HashNode<K, V>>> tempBuckets = new(Capacity);
        int index;
        foreach ((K key, V value) in GetHashNodes())
        {
            index = HashMap<K, V>.GetBucketIndex(key, Capacity);
            if (!tempBuckets.TryGetValue(index, out DoublyLinkedList<HashNode<K, V>>? bucket))
            {
                bucket = tempBuckets.Add(new(), index);
            }

            bucket!.AddToTail(new(key, value));
        }

        _buckets = tempBuckets;
    }

    private static int GetBucketIndex(K key, int capacity)
    {
        if (key is null)
        {
            throw new Exception("error. cannot call hash code. invalid key");
        }

        return GetAbsoluteValue(key.GetHashCode()) % capacity;
    }

    private static int GetAbsoluteValue(int value)
    {
        int signBitMask = value >> 31;

        value = (value ^ signBitMask) - signBitMask;

        return value;
    }
}