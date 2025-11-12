using System.Collections;

namespace tetris.Core.Library.DataStructures.Linear.Queues.DoublyEndedQueue;

public class Deque<T> : IEnumerable<T>
{
    private record LinkNode
    {
        public LinkNode(LinkNode? previous, T value, LinkNode? next)
        {
            Previous = previous;
            Value = value;
            Next = next;
        }

        public LinkNode(T value)
        {
            Value = value;
        }

        public LinkNode? Previous { get; set; }
        public T Value { get; set; }
        public LinkNode? Next { get; set; }
    }

    private LinkNode? _head;
    private LinkNode? _tail;
    private int _size;

    public void AddToFront(T value)
    {
        LinkNode newNode = new(value);
        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
            _size++;
            return;
        }

        LinkNode? next = _head.Next;
        newNode.Next = next;
        if (next != null)
        {
            next.Previous = newNode;
        }

        _head.Next = null;
        _head = newNode;
        _size++;
    }

    public void AddToRear(T value)
    {
        LinkNode newNode = new(value);
        if (_tail == null)
        {
            _tail = newNode;
            _head = newNode;
            _size++;
            return;
        }

        LinkNode? previous = _tail.Next;
        newNode.Previous = previous;
        if (previous != null)
        {
            previous.Next = newNode;
        }

        _tail.Next = null;
        _tail = newNode;
        _size++;
    }

    public T RemoveFromFront()
    {
        if (_head == null)
        {
            throw new ApplicationException("error. cannot remove. queue is empty");
        }

        LinkNode removed = _head;
        LinkNode? next = _head.Next;
        if (next != null)
        {
            next.Previous = null;
        }

        removed.Next = null;
        _head = next;
        _size--;

        return removed.Value;
    }

    public bool TryRemoveFromFront(out T? value)
    {
        value = default;

        if (_head == null)
        {
            return false;
        }

        LinkNode removed = _head;
        LinkNode? next = _head.Next;
        if (next != null)
        {
            next.Previous = null;
        }

        removed.Next = null;
        _head = next;
        value = removed.Value;
        _size--;

        return true;
    }

    public T RemoveFromRear()
    {
        if (_tail == null)
        {
            throw new ApplicationException("error. cannot remove. queue is empty");
        }

        LinkNode removed = _tail;
        LinkNode? previous = _tail.Previous;
        if (previous != null)
        {
            previous.Next = null;
        }

        removed.Previous = null;
        _tail = previous;
        _size--;

        return removed.Value;
    }

    public bool TryRemoveFromRear(out T? value)
    {
        value = default;

        if (_tail == null)
        {
            return false;
        }

        LinkNode removed = _tail;
        LinkNode? previous = _tail.Previous;
        if (previous != null)
        {
            previous.Next = null;
        }

        removed.Previous = null;
        _tail = previous;
        value = removed.Value;
        _size--;

        return true;
    }

    public void Purge()
    {
        while (!IsEmpty())
        {
            _ = RemoveFromFront();
        }
    }

    public bool IsEmpty() => _size == 0;

    public IEnumerator<T> GetEnumerator()
    {
        LinkNode? current = _head;
        while (current != null)
        {
            yield return current.Value;

            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}