using System.Collections;
using ccct.Core.Library.Linear.Arrays;
using ccct.Core.Library.Linear.Lists;
using static ccct.Core.Helpers.ApplicationHelper;

namespace ccct.Core.Library.NonLinear;

public class HashMap<K, V> : IEnumerable<(K, V)> where K : notnull
{
    private DynamicallyAllocatedArray<DoublyLinkedList<(K Key, V Value)>> _buckets;

    private const float LOAD_FACTOR = .7f;
    private const int INITIAL_SIZE = 2;

    private float _loadFactor;

    public int Size { get; private set; }

    public int Capacity { get; private set; }

    // TO add indexer here

    private HashMap(float loadFactor, int capacity)
    {
        _loadFactor = loadFactor;
        _buckets = new(capacity);
    }

    public HashMap(int capacity = INITIAL_SIZE) : this(LOAD_FACTOR, capacity)
    {
        if (capacity < 0 || capacity >= Array.MaxLength)
        {
            HandleError("error. invalid capacity value was given");
        }

        if (capacity == 0)
        {
            capacity = INITIAL_SIZE;
        }

        Capacity = capacity;
    }

    public void Add(K key, V value)
    {
        if (ContainsKey(key, out _))
        {
            throw new ApplicationException("error. cannot add. key already exist");
        }


    }

    public IEnumerator<(K, V)> GetEnumerator()
    {
        foreach (DoublyLinkedList<(K Key, V Value)> bucket in _buckets)
        {
            foreach ((K, V) item in bucket)
            {
                yield return item;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private bool ContainsKey(K key, out DoublyLinkedList<(K, V)>? bucket)
    {
        int index = HashMap<K, V>.GetAbsolute(key.GetHashCode()) % Capacity;
        if (_buckets[index] == null)
        {
            bucket = new();
            _buckets[index] = bucket;
            return false;
        }

        bucket = _buckets[index];

        return bucket.Exists(((K Key, V) item) =>
        item.Key != null && item.Key.Equals(key));
    }

    private static int GetAbsolute(int value)
    {
        int complement = value >> 31;
        return (value ^ complement) - complement;
    }
}