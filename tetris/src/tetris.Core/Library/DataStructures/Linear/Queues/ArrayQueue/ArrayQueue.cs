using System.Collections;

namespace tetris.Core.Library.DataStructures.Linear.Queues.ArrayQueue;

public class ArrayQueue<T> : IEnumerable<T>
{
    private readonly int _capacity;
    private readonly T[] _values;
    private int _size;
    private int _front;
    private int _rear;

    public ArrayQueue(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);

        _capacity = capacity;
        _values = new T[_capacity];
        _rear = -1;
    }

    public void Enqueue(T item)
    {
        if (IsFull())
        {
            throw new ApplicationException("error. cannot add. queue is full");
        }

        _rear = (_front + _size++) % _capacity;
        _values[_rear] = item;
    }

    public T Dequeue()
    {
        if (IsEmpty())
        {
            throw new ApplicationException("error. cannot remove. queue is empty");
        }

        T removed = _values[_front];
        _front = (_front + 1) % _capacity;
        _size--;

        return removed;
    }

    public bool TryDequeue(out T? dequeued)
    {
        dequeued = default;

        if (IsEmpty())
        {
            return false;
        }

        dequeued = _values[_front];
        _front = (_front + 1) % _capacity;
        _size--;

        return true;
    }

    public bool TryPeek(out T? peeked)
    {
        peeked = default;

        if (IsEmpty())
        {
            return false;
        }

        peeked = _values[_front];

        return true;
    }

    public IEnumerator<T> GetEnumerator()
    {
        int i = _front;
        do
        {
            yield return _values[i];
            if (i == _rear)
            {
                break;
            }

            i = (i + 1) % _capacity;
        }
        while (true);
    }

    private bool IsEmpty() => _size == 0;

    private bool IsFull() => _size == _capacity;

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}