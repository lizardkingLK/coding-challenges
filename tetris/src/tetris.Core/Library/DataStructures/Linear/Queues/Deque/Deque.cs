using System.Collections;

namespace tetris.Core.Library.DataStructures.Linear.Queues.Deque;

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

    public void AddToFront(T value)
    {
        LinkNode newNode = new(value);
        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
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
    }

    public void AddToRear(T value)
    {
        LinkNode newNode = new(value);
        if (_tail == null)
        {
            _tail = newNode;
            _head = newNode;
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