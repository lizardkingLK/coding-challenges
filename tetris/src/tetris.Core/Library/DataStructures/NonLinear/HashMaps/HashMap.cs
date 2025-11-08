using System.Collections;
using tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using static tetris.Core.Library.DataStructures.NonLinear.HashMaps.Shared.Constants;

namespace tetris.Core.Library.DataStructures.NonLinear.HashMaps;

public class HashMap<K, V> : IEnumerable<KeyValuePair<K, V>> where K : notnull
{
    private record HashNode(K Key, V? Value, bool IsActive = true)
    {
        public K Key { get; set; } = Key;
        public V? Value { get; set; } = Value;
        public bool IsActive { get; set; } = IsActive;
    }

    private readonly float _growthFactor;

    private DynamicallyAllocatedArray<HashNode> _buckets;

    public int Size { get; private set; }

    public int Capacity { get; private set; }

    public V? this[K key]
    {
        get => GetValue(key);
        set => Update(key, value);
    }

    public HashMap(float growthFactor = GrowthFactor)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(growthFactor);

        Capacity = InitialCapacity;
        _growthFactor = growthFactor;
        _buckets = new DynamicallyAllocatedArray<HashNode>(Capacity);
    }

    public HashMap(params KeyValuePair<K, V>[] keyValues) : this() => AddRange(keyValues);

    public HashMap(params (K, V)[] keyValues) : this() => AddRange(keyValues);

    public void AddRange(KeyValuePair<K, V>[] keyValues)
    {
        foreach ((K key, V value) in keyValues)
        {
            Add(key, value);
        }
    }

    public void AddRange((K, V)[] keyValues)
    {
        foreach ((K key, V value) in keyValues)
        {
            Add(key, value);
        }
    }

    public void Add(K key, V? value)
    {
        if (ContainsKey(key, out int index, out _))
        {
            throw new ApplicationException("error. cannot add. key already contains");
        }

        _buckets.Add(new(key, value), index);
        Size++;

        RehashIfSatisfies();
    }

    public bool TryAdd(K key, V? value)
    {
        if (ContainsKey(key, out int index, out _))
        {
            return false;
        }

        _buckets.Add(new(key, value), index);
        Size++;

        RehashIfSatisfies();

        return true;
    }

    public void Update(K key, V? value)
    {
        if (!ContainsKey(key, out _, out HashNode? bucket))
        {
            throw new ApplicationException("error. cannot update. bucket does not exist");
        }

        bucket!.Value = value;
    }

    public bool TryUpdate(K key, V? value)
    {
        if (!ContainsKey(key, out _, out HashNode? bucket))
        {
            return false;
        }

        bucket!.Value = value;

        return true;
    }

    public KeyValuePair<K, V> Remove(K key)
    {
        if (!ContainsKey(key, out _, out HashNode? bucket))
        {
            throw new ApplicationException("error. cannot remove. bucket does not exist");
        }

        bucket!.IsActive = false;

        return new(bucket.Key, bucket.Value!);
    }

    public bool TryRemove(K key, out KeyValuePair<K, V>? removed)
    {
        removed = default;

        if (!ContainsKey(key, out _, out HashNode? bucket))
        {
            return false;
        }

        bucket!.IsActive = false;
        removed = new(bucket.Key, bucket.Value!);

        return true;
    }

    public V? GetValue(K key)
    {
        if (!ContainsKey(key, out _, out HashNode? bucket))
        {
            throw new ApplicationException("error. cannot find. bucket does not exist");
        }

        return bucket!.Value;
    }

    public bool TryGetValue(K key, out V? value)
    {
        value = default;

        if (!ContainsKey(key, out _, out HashNode? bucket))
        {
            return false;
        }

        value = bucket!.Value;

        return true;
    }

    public IEnumerable<KeyValuePair<K, V>> GetKeyValues()
    {
        foreach (HashNode? bucket in _buckets)
        {
            if (bucket is { IsActive: true })
            {
                yield return new(bucket.Key, bucket.Value!);
            }
        }
    }

    public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
    {
        foreach (KeyValuePair<K, V> keyValue in GetKeyValues())
        {
            yield return keyValue;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private bool ContainsKey(
        K key,
        out int validIndex,
        out HashNode? bucket,
        Func<int>? getNextIndex = null)
    {
        getNextIndex ??= GetQuadraticProbing(key);

        validIndex = getNextIndex.Invoke();
        bool containsBucket = _buckets.TryGetValue(validIndex, out bucket);
        if (containsBucket && bucket is { IsActive: true } && bucket.Key.Equals(key))
        {
            return true;
        }
        else if (bucket is null)
        {
            return false;
        }

        return ContainsKey(key, out validIndex, out bucket, getNextIndex);
    }

    private Func<int> GetQuadraticProbing(K key)
    {
        int index = GetBucketIndex(key);
        int iteration = 0;

        return () => (index + (iteration + iteration * iteration++) / 2) % Capacity;
    }

    private void RehashIfSatisfies()
    {
        if ((float)Size / Capacity < _growthFactor)
        {
            return;
        }

        Capacity *= 2;
        DynamicallyAllocatedArray<HashNode> newBuckets = new(Capacity);
        int index;
        foreach ((K key, V value) in GetKeyValues())
        {
            index = GetBucketIndex(key);
            while (newBuckets.TryGetValue(index, out HashNode? bucket) && bucket is not null)
            {
                index = (index + 1) % Capacity;
            }

            newBuckets.Add(new(key, value), index);
        }

        _buckets = newBuckets;
    }

    private int GetBucketIndex(K key)
    {
        int hashCode = key.GetHashCode();
        int hashCodeMask = hashCode >> 31;
        hashCode = (hashCode ^ hashCodeMask) - hashCodeMask;

        return hashCode % Capacity;
    }
}