namespace pong.Core.Library.DataStructures.Linear.Queues.Deque;

public class Deque<T>
{
    public record LinkNode(LinkNode? Previous, T Value, LinkNode? Next)
    {
        public LinkNode(T value) : this(null, value, null) { }

        public LinkNode? Previous { get; set; } = Previous;
        public T Value { get; set; } = Value;
        public LinkNode? Next { get; set; } = Next;
    }

    public LinkNode? Head { get; set; }
    public LinkNode? Tail { get; set; }

    public IEnumerable<T> FrontToRear => GetValuesFrontToRear();
    public IEnumerable<T> RearToFront => GetValuesRearToFront();

    public LinkNode InsertToFront(T value)
    {
        LinkNode newNode = new(value);
        if (Head == null)
        {
            Head = newNode;
            Tail = newNode;
            return newNode;
        }

        newNode.Next = Head;
        Head.Previous = newNode;
        Head = newNode;

        return newNode;
    }

    public LinkNode InsertToRear(T value)
    {
        LinkNode newNode = new(value);
        if (Tail == null)
        {
            Tail = newNode;
            Head = newNode;
            return newNode;
        }

        newNode.Previous = Tail;
        Tail.Next = newNode;
        Tail = newNode;

        return newNode;
    }

    public LinkNode RemoveFromFront()
    {
        LinkNode removed;
        if (Head == null)
        {
            throw new ApplicationException("error. list is empty. cannot remove from front");
        }

        removed = Head;
        LinkNode? next = Head.Next;
        if (next != null)
        {
            next.Previous = null;
        }

        Head.Next = null;
        Head = next;

        return removed;
    }

    public LinkNode RemoveFromRear()
    {
        LinkNode removed;
        if (Tail == null)
        {
            throw new ApplicationException("error. list is empty. cannot remove from rear");
        }

        removed = Tail;
        LinkNode? previous = Tail.Previous;
        if (previous != null)
        {
            previous.Next = null;
        }

        Tail.Previous = null;
        Tail = previous;

        return removed;
    }

    public T SeekFront()
    {
        LinkNode head = Head
        ?? throw new ApplicationException(
            "error. list is empty. cannot seek front");

        return head.Value;
    }

    public bool TrySeekFront(out T? front)
    {
        front = default;

        if (Head == null)
        {
            return false;
        }

        front = Head.Value;

        return true;
    }

    public T SeekRear()
    {
        LinkNode tail = Tail
        ?? throw new ApplicationException(
            "error. list is empty. cannot seek rear");

        return tail.Value;
    }

    public bool TrySeekRear(out T? rear)
    {
        rear = default;

        if (Tail == null)
        {
            return false;
        }

        rear = Tail.Value;

        return true;
    }

    private IEnumerable<T> GetValuesFrontToRear()
    {
        LinkNode? current = Head;
        while (current != null)
        {
            yield return current.Value;

            current = current.Next;
        }
    }

    private IEnumerable<T> GetValuesRearToFront()
    {
        LinkNode? current = Tail;
        while (current != null)
        {
            yield return current.Value;

            current = current.Previous;
        }
    }
}