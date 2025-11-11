namespace tetris.Core.Library.DataStructures.Linear.Queues.ArrayQueue;

public class ArrayQueue<T>
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

    private bool IsEmpty() => _size == 0;

    private bool IsFull() => _size == _capacity;
}