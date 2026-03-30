using System.Collections;
using static ccct.Core.Helpers.ApplicationHelper;

namespace ccct.Core.Library.Linear.Arrays;

public class DynamicallyAllocatedArray<T> : IEnumerable<T>
{
    private const float GROWTH_FACTOR = .5f;
    private const int INITIAL_SIZE = 2;

    private float _growthFactor;
    private T[] _values;

    public int Size { get; private set; }

    public int Capacity { get; private set; }

    public T this[int index]
    {
        get => _values[index];
        set => _values[index] = value;
    }

    private DynamicallyAllocatedArray(float growthFactor, int capacity)
    {
        _growthFactor = growthFactor;
        _values = new T[capacity];
    }

    public DynamicallyAllocatedArray(int capacity = INITIAL_SIZE)
    : this(GROWTH_FACTOR, capacity)
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

    public void Add(T value)
    {
        _values[Size++] = value;
        ResizeIfSatisfies();
    }

    private void ResizeIfSatisfies()
    {
        if ((float)Size / Capacity < _growthFactor)
        {
            return;
        }

        T[] values = new T[Capacity * 2];
        for (int i = 0; i < Capacity; i++)
        {
            values[i] = _values[i];
        }

        Capacity *= 2;
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (T value in _values)
        {
            yield return value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}