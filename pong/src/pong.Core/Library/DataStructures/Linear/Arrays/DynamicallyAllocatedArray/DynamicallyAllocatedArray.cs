using static pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray.Shared.Constants;
using static pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray.Shared.Exceptions;

namespace pong.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;

public class DynamicallyAllocatedArray<T>
{
    private int _capacity;

    private T?[] _values;

    public int Size;

    public IEnumerable<T?> Values => GetValues();

    public T? this[int index]
    {
        get => GetValue(index);
        set => Update(index, value);
    }

    public DynamicallyAllocatedArray(int capacity = INITIAL_CAPACITY)
    {
        if (capacity < INITIAL_CAPACITY)
        {
            throw InvalidListSizeException;
        }

        _capacity = capacity;
        _values = new T[capacity];
    }

    public DynamicallyAllocatedArray(params T[] values) : this()
    {
        AddRange(values);
    }

    public DynamicallyAllocatedArray(
        params DynamicallyAllocatedArray<T>[] arrayList) : this()
    {
        foreach (DynamicallyAllocatedArray<T> array in arrayList)
        {
            AddRange(array);
        }
    }

    public T Add(T value)
    {
        GrowArrayIfSatisfies();

        _values[Size++] = value;

        return value;
    }

    public void AddRange(params T[] values)
    {
        foreach (T value in values)
        {
            Add(value);
        }
    }

    private void AddRange(DynamicallyAllocatedArray<T> array)
    {
        foreach (T? value in array.Values)
        {
            if (value is not null)
            {
                Add(value);
            }
        }
    }

    public T Insert(int index, T value)
    {
        if (index < 0 || index > _capacity - 1)
        {
            throw InvalidIndexException;
        }

        GrowArrayIfSatisfies();

        _values[index] = value;
        Size++;

        return value;
    }



    public T? Update(int index, T? value)
    {
        if (Size == 0)
        {
            throw ListEmptyException;
        }

        if (index < 0 || index > _capacity - 1)
        {
            throw InvalidIndexException;
        }

        _values[index] = value;

        return _values[index];
    }

    public bool TryUpdate(int index, T? value, out T? updated)
    {
        updated = default;

        if (Size == 0 || index < 0 || index > _capacity - 1)
        {
            return false;
        }

        updated = _values[index];
        _values[index] = value;

        return true;
    }

    public T? GetValue(int index)
    {
        if (Size == 0)
        {
            throw ListEmptyException;
        }

        if (index < 0 || index > _capacity - 1)
        {
            throw InvalidIndexException;
        }

        return _values[index];
    }

    public bool TryGetValue(int index, out T? value)
    {
        value = default;

        if (Size == 0 || index < 0 || index > _capacity - 1)
        {
            return false;
        }

        value = _values[index];

        return value is not null;
    }

    public bool TryGetValue(
        Predicate<T?> filterFunction,
        out T? value,
        out int? index)
    {
        value = default;
        index = default;

        if (Size == 0)
        {
            return false;
        }

        for (int i = 0; i < _capacity; i++)
        {
            value = _values[i];
            if (filterFunction.Invoke(value))
            {
                index = i;
                return true;
            }
        }

        return false;
    }

    public T? Remove()
    {
        if (Size == 0)
        {
            throw ListEmptyException;
        }

        T? removed = _values[Size - 1];
        _values[--Size] = default;

        ShrinkArrayIfSatisfies();

        return removed;
    }

    public T? Remove(T target)
    {
        if (Size == 0)
        {
            throw ListEmptyException;
        }

        if (!TryGetValue(item => item != null && item.Equals(target),
            out T? removed,
            out int? removedItemIndex))
        {
            throw ItemDoesNotExistException;
        }

        _values[removedItemIndex!.Value] = default;
        Size--;

        ShrinkArrayIfSatisfies();

        return removed;
    }

    public bool TryRemove(T target, out T? removed)
    {
        removed = default;

        if (Size == 0)
        {
            return false;
        }

        if (!TryGetValue(item => item != null && item.Equals(target),
            out removed,
            out int? removedIndex))
        {
            return false;
        }

        _values[removedIndex!.Value] = default;
        Size--;

        ShrinkArrayIfSatisfies();

        return true;
    }

    public T? Remove(int index)
    {
        if (Size == 0)
        {
            throw ListEmptyException;
        }

        if (index < 0 || index > _capacity - 1)
        {
            throw InvalidIndexException;
        }

        T? value = _values[index];
        _values[index] = default;

        for (int i = index; i < Size - 1; i++)
        {
            _values[i] = _values[i + 1];
            _values[i + 1] = default;
        }

        Size--;

        ShrinkArrayIfSatisfies();

        return value;
    }

    public bool TryRemove(int index, out T? removed)
    {
        removed = default;

        if (Size == 0 || index < 0 || index > _capacity - 1)
        {
            return false;
        }

        removed = _values[index];
        _values[index] = default;

        for (int i = index; i < Size - 1; i++)
        {
            _values[i] = _values[i + 1];
            _values[i + 1] = default;
        }

        Size--;

        ShrinkArrayIfSatisfies();

        return true;
    }

    private IEnumerable<T?> GetValues()
    {
        foreach (T? value in _values)
        {
            yield return value;
        }
    }

    private void GrowArrayIfSatisfies()
    {
        if ((float)Size / _capacity < GROWTH_FACTOR)
        {
            return;
        }

        int newCapacity = _capacity * 2;
        T?[] newValues = new T[newCapacity];
        for (int i = 0; i < _capacity; i++)
        {
            newValues[i] = _values[i];
        }

        _capacity = newCapacity;
        _values = newValues;
    }

    private void ShrinkArrayIfSatisfies()
    {
        if ((float)Size / _capacity > SHRINK_FACTOR)
        {
            return;
        }

        int newCapacity = Size;
        if (newCapacity == 0)
        {
            newCapacity = INITIAL_CAPACITY;
        }

        T?[] newValues = new T[newCapacity];
        for (int i = 0; i < newCapacity; i++)
        {
            newValues[i] = _values[i];
        }

        _capacity = newCapacity;
        _values = newValues;
    }
}