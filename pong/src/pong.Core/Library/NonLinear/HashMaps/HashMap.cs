using pong.Core.Library.Linear.Arrays.DynamicallyAllocatedArray;

namespace pong.Core.Library.NonLinear.HashMaps;

public class HashMap<K, V> where K : notnull
{
    private const float LOAD_FACTOR = .7f;

    public record HashNode(
        K Key,
        V Value,
        bool IsActive = true)
    {
        public V Value { get; set; } = Value;
        public bool IsActive { get; set; } = IsActive;
    }

    private int _capacity;
    private int _size;
    private readonly float _loadFactor;
    private DynamicallyAllocatedArray<HashNode> _buckets;

    public ApplicationException InvalidLoadFactorException = new("error. invalid load factor");
    public ApplicationException ExistingKeyException = new("error. key already contains");

    public int Count => _size;

    public const int INITIAL_CAPACITY = 2;

    public V this[K key]
    {
        get => Get(key);
        set => Update(key, value);
    }

    public HashMap(float loadFactor = LOAD_FACTOR)
    {
        if (loadFactor < 0 && loadFactor > 1)
        {
            throw InvalidLoadFactorException;
        }

        _loadFactor = loadFactor;
        _capacity = INITIAL_CAPACITY;
        _buckets = new(_capacity);
    }

    public HashMap(params KeyValuePair<K, V>[] keyValues)
    {
        _loadFactor = LOAD_FACTOR;
        _capacity = keyValues.Length;
        _buckets = new(_capacity);
        foreach ((K key, V value) in keyValues)
        {
            Add(key, value);
        }
    }

    public void Add(K key, V value)
    {
        if (ContainsKey(key, out int index, out _))
        {
            throw ExistingKeyException;
        }

        _buckets.Insert(index, new(key, value));
        _size++;

        ReHashIfSatisfies();
    }

    public bool TryAdd(K key, V value)
    {
        if (ContainsKey(key, out int index, out _))
        {
            return false;
        }

        _buckets.Insert(index, new(key, value));
        _size++;

        ReHashIfSatisfies();

        return true;
    }

    public void Update(K key, V newValue)
    {
        if (!ContainsKey(key, out _, out HashNode? hashNode))
        {
            throw ExistingKeyException;
        }

        hashNode!.Value = newValue;
    }

    public bool TryUpdate(K key, V newValue)
    {
        if (!ContainsKey(key, out _, out HashNode? hashNode))
        {
            return false;
        }

        hashNode!.Value = newValue;

        return true;
    }

    public HashNode Remove(K key)
    {
        if (!ContainsKey(key, out _, out HashNode? hashNode))
        {
            throw ExistingKeyException;
        }

        hashNode!.IsActive = false;

        return hashNode;
    }

    public bool TryRemove(K key, out HashNode? removed)
    {
        if (!ContainsKey(key, out _, out removed))
        {
            return false;
        }

        removed!.IsActive = false;

        return true;
    }

    public V Get(K key)
    {
        if (!ContainsKey(key, out _, out HashNode? hashNode))
        {
            throw ExistingKeyException;
        }

        return hashNode!.Value;
    }

    public bool TryGet(K key, out V? value)
    {
        value = default;

        if (!ContainsKey(key, out _, out HashNode? hashNode))
        {
            return false;
        }

        value = hashNode!.Value;

        return true;
    }

    private void ReHashIfSatisfies()
    {
        if ((float)_size / _capacity < _loadFactor)
        {
            return;
        }

        _capacity *= 2;
        DynamicallyAllocatedArray<HashNode> newBuckets = new(_capacity);
        int newIndex;
        foreach (HashNode? bucket in _buckets.Values)
        {
            if (bucket == null)
            {
                continue;
            }

            newIndex = GetBucketIndex(bucket.Key, newBuckets);
            newBuckets.Insert(newIndex, bucket);
        }

        _buckets = newBuckets;
    }

    private static int GetHashCodeValue(K key)
    {
        int hashCode = key.GetHashCode();
        int bitMask = hashCode >> 31;

        return (hashCode ^ bitMask) - bitMask;
    }

    private int GetBucketIndex(K key, DynamicallyAllocatedArray<HashNode> buckets)
    {
        int bucketIndex = GetHashCodeValue(key) % _capacity;
        while (buckets.TryGetValue(bucketIndex, out HashNode? bucket) && bucket != null)
        {
            bucketIndex = (bucketIndex + 1) % _capacity;
        }

        return bucketIndex;
    }

    private bool ContainsKey(
        K key,
        out int bucketIndex,
        out HashNode? bucket,
        int? index = null)
    {
        index ??= GetHashCodeValue(key) % _capacity;

        bucketIndex = index.Value;
        bool doesBucketContain = _buckets.TryGetValue(bucketIndex, out bucket);
        if (doesBucketContain && bucket!.Key!.Equals(key) && bucket.IsActive)
        {
            return true;
        }
        else if (bucket == null)
        {
            return false;
        }

        return ContainsKey(key, out bucketIndex, out bucket, bucketIndex + 1);
    }
}