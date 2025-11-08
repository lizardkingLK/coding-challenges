using System.Collections;
using static tetris.Core.Library.DataStructures.Linear.Shared.Constants;

namespace tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;

public class DynamicallyAllocatedArray<T> : IEnumerable<T?>
{
    private T?[] _values;
    private int _capacity;
    public int Size { get; private set; }

    public DynamicallyAllocatedArray(int capacity = InitialCapacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);

        _capacity = capacity;
        _values = new T[capacity];
    }

    public DynamicallyAllocatedArray(params T?[] values) : this(values.Length * 2)
    {
        int length = values.Length;
        for (int i = 0; i < length; i++, Size++)
        {
            _values[i] = values[i];
        }
    }

    public void AddRange(params T?[] values)
    {
        int length = values.Length;
        for (int i = 0; i < length; i++)
        {
            Add(values[i]);
        }
    }

    public void Add(T? value)
    {
        GrowArrayIfSatisfies();

        _values[Size++] = value;
    }

    public void Add(T? value, int index)
    {
        if (IsInvalidIndex(index))
        {
            throw new IndexOutOfRangeException("error. cannot add. invalid index");
        }

        GrowArrayIfSatisfies();

        _values[index] = value;
        Size++;
    }

    public bool TryAdd(T? value, int index)
    {
        if (IsInvalidIndex(index))
        {
            return false;
        }

        GrowArrayIfSatisfies();

        _values[index] = value;
        Size++;

        return true;
    }

    public T? Remove()
    {
        if (IsEmpty())
        {
            throw new ApplicationException("error. cannot remove. list is empty");
        }

        T? removed = _values[Size - 1];
        _values[Size--] = default;

        ShrinkArrayIfSatisfies();

        return removed;
    }

    public T? Remove(int index)
    {
        if (IsEmpty())
        {
            throw new ApplicationException("error. cannot remove. list is empty");
        }

        if (IsInvalidIndex(index))
        {
            throw new IndexOutOfRangeException("error. cannot remove. invalid index");
        }

        T? removed = _values[index];
        _values[index] = default;
        Size--;

        ShrinkArrayIfSatisfies();

        return removed;
    }

    public bool TryRemove(int index, out T? removed)
    {
        removed = default;

        if (IsEmpty() || IsInvalidIndex(index))
        {
            return false;
        }

        removed = _values[index];
        _values[index] = default;
        Size--;

        ShrinkArrayIfSatisfies();

        return true;
    }

    public bool Exists(Predicate<T?> lookup, out int? index, out T? value)
    {
        index = default;
        value = default;

        T? item;
        for (int i = 0; i < _values.Length; i++)
        {
            item = _values[i];
            if (lookup.Invoke(item))
            {
                index = i;
                value = item;

                return true;
            }
        }

        return false;
    }

    public IEnumerator<T?> GetEnumerator()
    {
        for (int i = 0; i < Size; i++)
        {
            yield return _values[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private bool IsEmpty() => Size == 0;

    private bool IsInvalidIndex(int index) => index < 0 || index >= _capacity;

    private void GrowArrayIfSatisfies()
    {
        if ((float)Size / _capacity < GrowthFactor)
        {
            return;
        }

        _capacity *= 2;
        T?[] newValues = new T[_capacity];
        for (int i = 0; i < Size; i++)
        {
            newValues[i] = _values[i];
        }

        _values = newValues;
    }

    private void ShrinkArrayIfSatisfies()
    {
        if ((float)Size / _capacity > ShrinkFactor)
        {
            return;
        }

        _capacity = Size > InitialCapacity ? Size : InitialCapacity;
        T?[] newValues = new T[_capacity];
        for (int i = 0; i < Size; i++)
        {
            newValues[i] = _values[i];
        }

        _values = newValues;
    }
}