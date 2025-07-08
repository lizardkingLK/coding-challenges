namespace snakeGame.Core.Library;

public class DynamicArray<T>
{
    public int Capacity { get; private set; } = 1;

    public int Size { get; private set; } = 0;

    private T?[] values;

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

    public void Display(bool? shouldIncludeCapacity = false)
    {
        int length = shouldIncludeCapacity == true ? Capacity : Size;
        for (int i = 0; i < length; i++)
        {
            Console.Write("{0} ", values[i]);
        }
    }

    public T? GetValue(int index)
    {
        if (index < 0 || index > Size - 1)
        {
            throw new Exception("error. index is invalid");
        }

        return values[index];
    }

    public bool Search(Func<T?, bool> searchFunction, int startingIndex, out int index, out T? value)
    {
        index = 0;
        value = default;

        if (Size == 0 || startingIndex > Size)
        {
            return false;
        }

        int i = startingIndex;
        while (i < Size)
        {
            if (searchFunction(values[i]))
            {
                index = i;
                value = values[i];
                return true;
            }

            i++;
        }

        return false;
    }

    public bool GetRandom(Func<T?, bool> searchFunction, ref int index, out T? value)
    {
        value = default;

        if (Size == 0)
        {
            return false;
        }

        int i = 0;
        DynamicArray<T?> tempValues = new();
        while (i < Size)
        {
            if (searchFunction(values[i]))
            {
                value = values[i];
                tempValues.Add(value);
            }

            i++;
        }

        i = new Random().Next(0, tempValues.Size);
        index = i;
        value = tempValues.GetValue(i);

        return true;
    }

    public bool Replace(Func<T?, bool> searchFunction, T? value)
    {
        if (Size == 0)
        {
            return false;
        }

        int i = 0;
        while (i < Size)
        {
            if (searchFunction(values[i]))
            {
                values[i] = value;
                return true;
            }

            i++;
        }

        return true;
    }

    public bool Replace(int index, T value)
    {
        if (Size == 0)
        {
            return false;
        }

        if (index < 0 || index > Size - 1)
        {
            return false;
        }

        values[index] = value;

        return true;
    }

    public IEnumerable<T?> GetValues()
    {
        for (int i = 0; i < Size; i++)
        {
            yield return values[i];
        }
    }
}