using System.Collections;
using static ccct.Core.Shared.Errors;

namespace ccct.Core.Library.Linear.Arrays;

public class DynamicallyAllocatedArray<T> : IEnumerable<T>
{
    private const float GROWTH_FACTOR = .5f;
    private const int INITIAL_SIZE = 2;

    private readonly float _growthFactor;
    private T[] _values;

    public int Size { get; private set; }

    public int Capacity { get; private set; }

    public T this[int index]
    {
        get => _values[index];
        set => _values[index] = value;
    }

    public DynamicallyAllocatedArray(int capacity = INITIAL_SIZE)
    {
        if (capacity < 0 || capacity >= Array.MaxLength)
        {
            throw new ApplicationException(ErrorDynamicallyAllocatedArrayInvalidCapacity);
        }

        if (capacity == 0)
        {
            capacity = INITIAL_SIZE;
        }

        Capacity = capacity;
        _growthFactor = GROWTH_FACTOR;
        _values = new T[capacity];
    }

    public void Add(T value)
    {
        _values[Size++] = value;
        ResizeIfSatisfies();
    }

    private void ResizeIfSatisfies()
    {
        if ((float)Size / Capacity <= _growthFactor)
        {
            return;
        }

        int newCapacity = Capacity * 2;
        T[] values = new T[newCapacity];
        for (int i = 0; i < Capacity; i++)
        {
            values[i] = _values[i];
        }

        _values = values;
        Capacity = newCapacity;
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