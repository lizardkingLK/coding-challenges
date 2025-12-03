using tetris.Core.Library.DataStructures.Linear.Lists.Shared.State;

namespace tetris.Core.Library.DataStructures.Linear.Lists;

public class DoublyLinkedList<T>
{
    public LinkNode<T>? Head;
    public LinkNode<T>? Tail;
    public IEnumerable<T> ValuesHeadToTail => TraverseForward();
    public IEnumerable<T> ValuesTailToHead => TraverseBackward();

    public LinkNode<T> AddToHead(T value)
    {
        LinkNode<T> newNode = new(value);
        if (Head == null)
        {
            Head = newNode;
            if (Tail == null)
            {
                Tail = newNode;
            }

            return newNode;
        }

        newNode.Next = Head;
        Head.Previous = newNode;
        Head = newNode;

        return newNode;
    }

    public LinkNode<T> AddToTail(T value)
    {
        LinkNode<T> newNode = new(value);
        if (Tail == null)
        {
            Tail = newNode;
            if (Head == null)
            {
                Head = newNode;
            }

            return newNode;
        }

        newNode.Previous = Tail;
        Tail.Next = newNode;
        Tail = newNode;

        return newNode;
    }

    public T RemoveFromFront()
    {
        LinkNode<T> headNode = Head
            ?? throw new Exception("error. cannot remove from front. list is empty");
        if (headNode.Next != null)
        {
            headNode.Next.Previous = null;
        }

        Head = headNode.Next;
        headNode.Next = null;

        return headNode.Value;
    }

    public T RemoveFromRear()
    {
        LinkNode<T> tailNode = Tail
            ?? throw new Exception("error. cannot remove from rear. list is empty");
        if (tailNode.Previous != null)
        {
            tailNode.Previous.Next = null;
        }

        Tail = tailNode.Previous;
        tailNode.Previous = null;

        return tailNode.Value;
    }

    public LinkNode<T> AddBefore(LinkNode<T> target, T value)
    {
        if (!TryGetLinkNode(target, out LinkNode<T>? current))
        {
            throw new ApplicationException("error. cannot add. target not found");
        }

        LinkNode<T> newNode = new(value);
        LinkNode<T>? previous = current!.Previous;
        if (current == Head)
        {
            Head = newNode;
        }

        current.Previous = newNode;
        newNode.Next = current;
        newNode.Previous = previous;
        if (previous != null)
        {
            previous.Next = newNode;
        }

        return newNode;
    }

    public LinkNode<T> AddAfter(LinkNode<T> target, T value)
    {
        if (!TryGetLinkNode(target, out LinkNode<T>? current))
        {
            throw new ApplicationException("error. cannot add. target not found");
        }

        LinkNode<T> newNode = new(value);
        LinkNode<T>? next = current!.Next;
        if (current == Tail)
        {
            Tail = newNode;
        }

        current.Next = newNode;
        newNode.Previous = current;
        newNode.Next = next;
        if (next != null)
        {
            next.Previous = newNode;
        }

        return newNode;
    }

    public LinkNode<T> Remove(T targetValue)
    {
        if (!TryGetLinkNode(targetValue, out LinkNode<T>? current))
        {
            throw new ApplicationException("error. cannot remove. target not found");
        }

        LinkNode<T> removed = current!;
        LinkNode<T>? previous = removed.Previous;
        LinkNode<T>? next = removed.Next;

        removed.Next = null;
        removed.Previous = null;

        if (previous == null)
        {
            Head = next;
        }
        else
        {
            previous.Next = next;
        }

        if (next == null)
        {
            Tail = previous;
        }
        else
        {
            next.Previous = previous;
        }

        return removed;
    }

    public LinkNode<T> RemoveBefore(LinkNode<T> target)
    {
        if (!TryGetLinkNode(target, out LinkNode<T>? current))
        {
            throw new ApplicationException("error. cannot remove. target not found");
        }

        if (current?.Previous == null)
        {
            throw new ApplicationException("error. cannot remove. target not found");
        }

        LinkNode<T>? removed = current.Previous;
        if (removed == Head)
        {
            Head = current;
        }

        LinkNode<T>? previous = removed.Previous;
        removed.Next = null;
        removed.Previous = null;
        if (previous != null)
        {
            previous.Next = current;
        }

        current.Previous = previous;

        return removed;
    }

    public LinkNode<T> RemoveAfter(LinkNode<T> target)
    {
        if (!TryGetLinkNode(target, out LinkNode<T>? current))
        {
            throw new ApplicationException("error. cannot remove. target not found");
        }

        if (current?.Next == null)
        {
            throw new ApplicationException("error. cannot remove. target not found");
        }

        LinkNode<T>? removed = current.Next;
        if (removed == Tail)
        {
            Tail = current;
        }

        LinkNode<T>? next = removed.Next;
        removed.Next = null;
        removed.Previous = null;
        if (next != null)
        {
            next.Previous = current;
        }

        current.Next = next;

        return removed;
    }

    public bool TryGetValue(Predicate<T> filterFunction, out T? value)
    {
        value = default;

        foreach (T item in ValuesHeadToTail)
        {
            if (filterFunction.Invoke(item))
            {
                value = item;

                return true;
            }
        }

        return false;
    }

    private IEnumerable<T> TraverseForward()
    {
        LinkNode<T>? current = Head;
        while (current != null)
        {
            yield return current.Value;

            current = current.Next;
        }
    }

    private IEnumerable<T> TraverseBackward()
    {
        LinkNode<T>? current = Tail;
        while (current != null)
        {
            yield return current.Value;

            current = current.Previous;
        }
    }

    private bool TryGetLinkNode(LinkNode<T> target, out LinkNode<T>? current)
    {
        current = Head;

        while (current != null)
        {
            if (current == target)
            {
                return true;
            }

            current = current?.Next;
        }

        return false;
    }

    private bool TryGetLinkNode(T? targetValue, out LinkNode<T>? current)
    {
        current = Head;

        while (current != null)
        {
            if (current.Value != null && current.Value.Equals(targetValue))
            {
                return true;
            }

            current = current?.Next;
        }

        return false;
    }
}