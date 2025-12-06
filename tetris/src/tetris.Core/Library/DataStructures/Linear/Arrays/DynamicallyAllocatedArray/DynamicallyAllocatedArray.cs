using System.Collections;
using static tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray.Shared.Constants;

namespace tetris.Core.Library.DataStructures.Linear.Arrays.DynamicallyAllocatedArray;

public class DynamicallyAllocatedArray<T> : IEnumerable<T?>
{
    private T?[] _values;
    private int _capacity;
    public int Size { get; private set; }

    public T? this[int index]
    {
        get => GetValue(index);
        set => Update(value, index);
    }

    public IEnumerable<T?> Values => GetValues();

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

    public T Add(T? value, int index)
    {
        if (IsInvalidIndex(index))
        {
            throw new IndexOutOfRangeException("error. cannot add. invalid index");
        }

        GrowArrayIfSatisfies();

        _values[index] = value;
        Size++;

        return value!;
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

    public void Update(T? value, int index)
    {
        if (IsEmpty())
        {
            throw new ApplicationException("error. cannot update. list is empty");
        }

        if (IsInvalidIndex(index))
        {
            throw new IndexOutOfRangeException("error. cannot updated. invalid index");
        }

        _values[index] = value;
    }

    public bool TryUpdate(T? value, int index)
    {
        if (IsEmpty() || IsInvalidIndex(index))
        {
            return false;
        }

        _values[index] = value;

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

    public T? GetValue(int index)
    {
        if (IsEmpty())
        {
            throw new ApplicationException("error. cannot find. list is empty");
        }

        if (IsInvalidIndex(index))
        {
            throw new IndexOutOfRangeException("error. cannot find. invalid index");
        }

        return _values[index];
    }

    public bool TryGetValue(int index, out T? value)
    {
        value = default;

        if (IsEmpty() || IsInvalidIndex(index))
        {
            return false;
        }

        value = _values[index];

        return value is not null;
    }

    public void Shuffle()
    {
        int randomIndex;
        for (int i = 0; i < Size; i++)
        {
            randomIndex = Random.Shared.Next(Size);
            (_values[i], _values[randomIndex]) = (_values[randomIndex], _values[i]);
        }
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
        for (int i = 0; i < _capacity; i++)
        {
            yield return _values[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private IEnumerable<T?> GetValues()
    {
        for (int i = 0; i < _capacity; i++)
        {
            yield return _values[i];
        }
    }

    private bool IsEmpty() => Size == 0;

    private bool IsInvalidIndex(int index) => index < 0 || index >= _capacity;

    private void GrowArrayIfSatisfies()
    {
        if ((float)Size / _capacity < GrowthFactor)
        {
            return;
        }

        T?[] newValues = new T[_capacity * 2];
        for (int i = 0; i < _capacity; i++)
        {
            newValues[i] = _values[i];
        }

        _capacity *= 2;
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