namespace snakeGame.Core.Library;

public class Deque<T>(T head)
{
    private LinkNode<T>? front = new(head);
    private LinkNode<T>? rear = new(head);

    public int Size { get; set; }

    public void InsertToRear(T item)
    {
        LinkNode<T>? tailNode = front;
        if (tailNode == null)
        {
            front = new LinkNode<T>(item);
            rear = front;
            return;
        }

        tailNode = new LinkNode<T>(item);
        rear!.Next = tailNode;
        tailNode.Previous = rear;
        rear = tailNode;
        Size++;
    }

    public void InsertToFront(T item)
    {
        LinkNode<T>? headNode = front;
        if (headNode == null)
        {
            front = new LinkNode<T>(item);
            rear = front;
            return;
        }

        headNode = new LinkNode<T>(item);
        front!.Previous = headNode;
        headNode.Next = front;
        front = headNode;
        Size++;
    }

    public void DisplayFrontToRear()
    {
        LinkNode<T>? headNode = front;
        while (headNode != null)
        {
            Console.Write($"{headNode.Value} ");
            headNode = headNode.Next;
        }

        Console.WriteLine();
    }

    public T RemoveFromRear()
    {
        LinkNode<T>? tailNode = rear
            ?? throw new Exception("error. cannot remove from rear. deque is empty");

        tailNode.Previous!.Next = null;
        rear = tailNode.Previous;
        tailNode.Previous = null;

        return tailNode.Value;
    }

    public T RemoveFromFront()
    {
        LinkNode<T>? headNode = front
            ?? throw new Exception("error. cannot remove from front. deque is empty");

        headNode.Next!.Previous = null;
        front = headNode.Next;
        headNode.Next = null;

        return headNode.Value;
    }

    public void DisplayRearToFront()
    {
        LinkNode<T>? tailNode = rear;
        while (tailNode != null)
        {
            Console.Write($"{tailNode.Value} ");
            tailNode = tailNode.Previous;
        }

        Console.WriteLine();
    }

    public T SeekRear()
    {
        LinkNode<T>? tailNode = rear
            ?? throw new Exception("error. cannot seek at rear. deque is empty");

        return tailNode.Value;
    }

    public T SeekFront()
    {
        LinkNode<T>? headNode = front
            ?? throw new Exception("error. cannot seek at front. deque is empty");

        return headNode.Value;
    }
}
