namespace pong.Core.Library.DataStructures.Linear.Lists.DoublyLinkedList;

public class DoublyLinkedList<T>
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

    public void AddToFront(T value)
    {
        LinkNode newNode = new(value);
        if (Head == null)
        {
            Head = newNode;
            Tail = newNode;
            return;
        }

        newNode.Next = Head;
        Head.Previous = newNode;
        Head = newNode;
    }

    public void AddToRear(T value)
    {
        LinkNode newNode = new(value);
        if (Tail == null)
        {
            Tail = newNode;
            Head = newNode;
            return;
        }

        newNode.Previous = Tail;
        Tail.Next = newNode;
        Tail = newNode;
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
}