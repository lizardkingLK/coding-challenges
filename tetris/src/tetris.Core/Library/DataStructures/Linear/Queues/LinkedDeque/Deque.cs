using System.Collections;

namespace tetris.Core.Library.DataStructures.Linear.Queues.LinkedDeque;

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

    public int Size { get; set; }

    private readonly Lock _lock = new();

    public void AddToFront(T value)
    {
        // lock (_lock)
        // {
        LinkNode newNode = new(value);
        LinkNode? next;
        if (_head == null)
        {
            _head = newNode;
            // _tail = _head;
            if (_tail == null)
            {
                return;
            }

            LinkNode? current = _tail;
            next = null;
            while (current != null)
            {
                next = current;
                current = current.Next;
            }

            next!.Previous = _head;
            _head.Next = next;

            return;
        }

        _head!.Previous = newNode;
        newNode.Next = _head;
        _head = newNode;
        Size++;
        // }
    }

    public void AddToRear(T value)
    {
        // lock (_lock)
        // {
        LinkNode newNode = new(value);
        LinkNode? previous;
        if (_tail == null)
        {
            _tail = newNode;
            // _head = _tail;
            Size++;
            if (_head == null)
            {
                return;
            }

            LinkNode? current = _head;
            previous = null;
            while (current != null)
            {
                previous = current;
                current = current.Next;
            }

            previous!.Next = _tail;
            _tail.Previous = previous;

            return;
        }

        _tail!.Next = newNode;
        newNode.Previous = _tail;
        _tail = newNode;
        Size++;
        // }
    }

    public T RemoveFromFront()
    {
        // lock (_lock)
        // {
        LinkNode removed = _head
            ?? throw new ApplicationException("error. cannot remove. queue is empty");
        if (removed.Next != null)
        {
            removed.Next.Previous = null;
        }

        _head = removed.Next;
        removed.Next = null;
        Size--;

        return removed.Value;
        // }
    }

    public bool TryRemoveFromFront(out T? value)
    {
        // lock (_lock)
        // {
        value = default;

        LinkNode? removed = _head ?? _tail;
        if (removed == null)
        {
            return false;
        }

        if (removed.Next != null)
        {
            removed.Next.Previous = null;
        }

        _head = removed.Next;
        removed.Next = null;
        value = removed.Value;
        Size--;

        return true;
        // }
    }

    public T RemoveFromRear()
    {
        // lock (_lock)
        // {
        LinkNode? removed = _tail
            ?? throw new ApplicationException("error. cannot remove. queue is empty");
        if (removed.Previous != null)
        {
            removed.Previous.Next = null;
        }

        _tail = removed.Previous;
        removed.Previous = null;
        Size--;

        return removed.Value;
        // }
    }

    public bool TryRemoveFromRear(out T? value)
    {
        // lock (_lock)
        // {
        value = default;

        LinkNode? removed = _tail ?? _head;
        if (removed == null)
        {
            return false;
        }

        if (removed.Previous != null)
        {
            removed.Previous.Next = null;
        }

        _tail = removed.Previous;
        removed.Previous = null;
        value = removed.Value;
        Size--;

        return true;
        // }
    }

    public void Purge()
    {
        // lock (_lock)
        // {
        while (TryRemoveFromFront(out T? _))
        {
            break;
        }

        while (TryRemoveFromFront(out T? _))
        {
            break;
        }
        // }
    }

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