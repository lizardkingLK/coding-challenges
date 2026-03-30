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

    public bool Exists(Predicate<T> check)
    {
        LinkNode? current = _head;
        while (current != null)
        {
            if (check.Invoke(current.Value))
            {
                return true;
            }

            current = current.Next;
        }

        return false;
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