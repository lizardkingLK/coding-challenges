using System.Collections;

namespace ccct.Core.Library.Linear.Lists;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    public record LinkNode(T Value)
    {
        public LinkNode? Previous { get; set; }
        public LinkNode? Next { get; set; }
    }

    private LinkNode? _head;
    private LinkNode? _tail;

    public IEnumerable<T> Values => GetValues();

    public LinkNode AddToFront(T value)
    {
        LinkNode newNode = new(value);
        if (_head == null)
        {
            _head = newNode;
            if (_tail == null)
            {
                _tail = newNode;
            }

            return newNode;
        }

        newNode.Next = _head;
        _head.Previous = newNode;
        _head = newNode;

        return newNode;
    }

    public LinkNode AddToRear(T value)
    {
        LinkNode newNode = new(value);
        if (_tail == null)
        {
            _tail = newNode;
            if (_head == null)
            {
                _head = newNode;
            }

            return newNode;
        }

        newNode.Previous = _tail;
        _tail.Next = newNode;
        _tail = newNode;

        return newNode;
    }

    public bool Exists(Predicate<T> check)
    {
        foreach (T value in Values)
        {
            if (check.Invoke(value))
            {
                return true;
            }
        }

        return false;
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (T value in Values)
        {
            yield return value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private IEnumerable<T> GetValues()
    {
        LinkNode? current = _head;
        while (current != null)
        {
            yield return current.Value;

            current = current.Next;
        }
    }
}