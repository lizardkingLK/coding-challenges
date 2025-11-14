namespace tetris.Core.Library.DataStructures.Linear.Stacks.LinkedStack;

public class LinkedStack<T>
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

    public int Size { get; private set; }

    public void Push(T value)
    {
        if (_head == null)
        {
            _head = new(value);
            Size++;
            return;
        }

        LinkNode newNode = new(value);
        _head.Previous = newNode;
        newNode.Next = _head;
        _head = newNode;
        Size++;
    }

    public T Pop()
    {
        if (_head == null)
        {
            throw new ApplicationException("error. cannot pop. stack is empty");
        }

        LinkNode popped = _head;
        LinkNode? next = _head.Next;
        if (next != null)
        {
            next.Previous = null;
        }

        _head = next;
        popped.Next = null;
        Size--;

        return popped.Value;
    }

    public bool TryPop(out T? value)
    {
        value = default;

        if (_head == null)
        {
            return false;
        }

        LinkNode popped = _head;
        LinkNode? next = _head.Next;
        if (next != null)
        {
            next.Previous = null;
        }

        _head = next;
        popped.Next = null;
        Size--;

        value = popped.Value;

        return true;
    }

    public T Peek()
    {
        if (_head == null)
        {
            throw new ApplicationException("error. cannot peek. stack is empty");
        }

        return _head.Value;
    }

    public bool TryPeek(out T? value)
    {
        value = default;

        if (_head == null)
        {
            return false;
        }

        value = _head.Value;

        return true;
    }

    public void Purge()
    {
        while (!TryPop(out _)) { }
    }
}