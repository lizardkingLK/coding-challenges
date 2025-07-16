namespace snakeGame.Core.Library;

public class DynamicArray<T>
{
    public int Capacity { get; private set; } = 1;

    public int Size { get; private set; } = 0;

    public T?[] values;

    public DynamicArray()
    {
        values = new T[Capacity];
    }

    public DynamicArray(int capacity)
    {
        if (capacity <= 0)
        {
            throw new Exception("error. invalid capacity argument provided");
        }

        Capacity = capacity;
        values = new T[capacity];
    }

    public void Add(T value)
    {
        if (value == null)
        {
            throw new Exception("error. invalid value argument provided");
        }

        if (Size == Capacity)
        {
            GrowArray();
        }

        values[Size++] = value;
    }

    public void Insert(int index, T value)
    {
        if (index < 0 || index > Size)
        {
            throw new Exception("error. index is invalid");
        }

        if (value == null)
        {
            throw new Exception("error. value is invalid");
        }

        if (Size == Capacity)
        {
            GrowArray();
        }

        int length = Size - index;
        T?[] tempArray = new T[length];
        for (int i = 0; i < length; i++)
        {
            tempArray[i] = values[index + i];
        }

        values[index] = value;
        for (int i = 0; i < length; i++)
        {
            values[index + 1 + i] = tempArray[i];
        }

        Size++;
    }

    public T? Delete()
    {
        if (Size == 0)
        {
            throw new Exception("error. cannot delete, array is empty");
        }

        T? removed = values[Size - 1];
        values[Size - 1] = default;
        Size--;
        if (Size <= Capacity / 3)
        {
            ShrinkArray();
        }

        return removed;
    }

    public T? Remove(int index)
    {
        if (Size == 0)
        {
            throw new Exception("error. cannot remove, array is empty");
        }

        if (index < 0 || index > Size - 1)
        {
            throw new Exception("error. index is invalid");
        }

        T? removed = values[index];
        values[index] = default;

        int length = Size - index - 1;
        T? value;
        for (int i = 0; i < length; i++)
        {
            value = values[index + 1 + i];
            values[index + 1 + i] = default;
            values[index + i] = value;
        }

        Size--;
        if (Size <= Capacity / 3)
        {
            ShrinkArray();
        }

        return removed;
    }

    public T? Remove(T? value)
    {
        if (Size == 0)
        {
            throw new Exception("error. cannot remove, array is empty");
        }

        T? removed = default;
        int i;
        for (i = 0; i < Size; i++)
        {
            removed = values[i];
            if (ReferenceEquals(value, removed))
            {
                break;
            }
        }

        int index = i;

        values[index] = default;
        int length = Size - i - 1;
        for (i = 0; i < length; i++)
        {
            value = values[index + 1 + i];
            values[index + 1 + i] = default;
            values[index + i] = value;
        }

        Size--;
        if (Size <= Capacity / 3)
        {
            ShrinkArray();
        }

        return removed;
    }

    public T? Remove(Func<T, bool> searchFunction)
    {
        for (int i = 0; i < Size; i++)
        {
            if (searchFunction(values[i]!))
            {
                return Remove(i);
            }
        }

        return default;
    }

    public T? GetValue(int index)
    {
        if (Size == 0)
        {
            throw new Exception("error. array is empty");
        }

        if (index < 0 || index > Size - 1)
        {
            throw new Exception("error. index is invalid");
        }

        return values[index];
    }

    public bool TryGetValue(int index, out T? value)
    {
        value = default;

        if (Size == 0 || index < 0 || index > Size - 1)
        {
            return false;
        }

        value = values[index];

        return true;
    }

    public T?[] ToArray()
    {
        return values;
    }

    private void GrowArray()
    {
        T?[] tempArray = new T[Capacity * 2];
        int i;
        for (i = 0; i < Capacity; i++)
        {
            tempArray[i] = values[i];
        }

        values = tempArray;

        Capacity *= 2;
    }

    private void ShrinkArray()
    {
        Capacity = Size * 2;
        T?[] tempArray = new T[Capacity];
        for (int i = 0; i < Size; i++)
        {
            tempArray[i] = values[i];
        }

        values = tempArray;
    }
}