using pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;
using static pong.Core.Library.DataStructures.NonLinear.HashMaps.Shared.Constants;

namespace pong.Core.Library.DataStructures.NonLinear.HashMaps;

public class HashMap<K, V>(float loadFactor = LOAD_FACTOR) where K : notnull
{
    public record HashNode(
        K Key,
        V Value,
        bool IsActive = true)
    {
        public V Value { get; set; } = Value;
        public bool IsActive { get; set; } = IsActive;

        public void Deconstruct(out K key, out V value)
        {
            key = Key;
            value = Value;
        }
    }

    private readonly float _loadFactor = loadFactor;

    private DynamicallyAllocatedArray<HashNode?> _buckets = new();

    private int _capacity = INITIAL_CAPACITY;

    public int Size { get; private set; }

    public V this[K key]
    {
        get => Get(key);
        set => Update(key, value);
    }

    public HashMap(params KeyValuePair<K, V>[] keyValues) : this()
    {
        foreach ((K key, V value) in keyValues)
        {
            Add(key, value);
        }
    }

    public void Add(K key, V value)
    {
        if (ContainsKey(key, out int index, out _))
        {
            throw new Exception("error. cannot add value. key already contain");
        }

        _buckets.Insert(index, new(key, value));
        Size++;

        ReHashIfSatisfies();
    }

    public V Get(K key)
    {
        if (!ContainsKey(key, out _, out HashNode? value))
        {
            throw new Exception("error. cannot get value. key does not contain");
        }

        return value!.Value;
    }

    public IEnumerable<HashNode> GetHashNodes()
    {
        foreach (HashNode? bucket in _buckets.Values)
        {
            if (bucket is { IsActive: true })
            {
                yield return bucket;
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

    public V Remove(K key)
    {
        if (!ContainsKey(key, out int index, out HashNode? value))
        {
            throw new Exception("error. cannot remove value. key does not contain");
        }

        value!.IsActive = false;

        _buckets.Update(index, value);

        return value!.Value;
    }

    public bool TryAdd(K key, V value)
    {
        if (ContainsKey(key, out int index, out _))
        {
            return false;
        }

        _buckets.Insert(index, new(key, value));
        Size++;

        ReHashIfSatisfies();

        return true;
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

    public bool TryRemove(K key, out V? value)
    {
        value = default;

        if (!ContainsKey(key, out int index, out HashNode? hashNode))
        {
            return false;
        }

        hashNode!.IsActive = false;

        _buckets.Update(index, hashNode);

        value = hashNode!.Value;

        return true;
    }

    public bool TryUpdate(K key, V value)
    {
        if (!ContainsKey(key, out _, out HashNode? hashNode))
        {
            return false;
        }

        hashNode!.Value = value;

        return true;
    }

    public void Update(K key, V value)
    {
        if (!ContainsKey(key, out _, out HashNode? hashNode))
        {
            throw new Exception("error. cannot update value. key does not contain");
        }

        hashNode!.Value = value;
    }

    private bool ContainsKey(
        K key,
        out int validIndex,
        out HashNode? value,
        int? index = null)
    {
        index ??= GetBucketIndex(key);

        validIndex = index.Value % _capacity;
        bool doesBucketContain = _buckets.TryGetValue(validIndex, out value);
        if (doesBucketContain && value!.Key!.Equals(key) && value.IsActive)
        {
            return true;
        }
        else if (value is null)
        {
            return false;
        }

        return ContainsKey(key, out validIndex, out value, validIndex + 1);
    }

    private int GetBucketIndex(K key)
    {
        int hashCode = key.GetHashCode();
        int bitMask = hashCode >> 31;
        hashCode = (hashCode ^ bitMask) - bitMask;

        return hashCode % _capacity;
    }

    private void ReHashIfSatisfies()
    {
        if ((float)Size / _capacity < _loadFactor)
        {
            return;
        }

        _capacity *= 2;
        DynamicallyAllocatedArray<HashNode?> tempBuckets = new(_capacity);
        int index;
        foreach (HashNode bucket in GetHashNodes())
        {
            index = GetBucketIndex(bucket.Key);
            while (tempBuckets.TryGetValue(index, out _))
            {
                index = (index + 1) % _capacity;
            }

            tempBuckets.Insert(index, bucket);
        }

        _buckets = tempBuckets;
    }
}