namespace snakeGame.Core.Library;

public class Deque<T>
{
    private LinkNode<T>? front;
    private LinkNode<T>? rear;
    public int Size { get; set; }

    public Deque(T head)
    {
        front = new(head);
        rear = front;
        Size++;
    }

    public void InsertToRear(T item)
    {
        LinkNode<T>? tailNode = rear;
        if (tailNode == null)
        {
            front = new LinkNode<T>(item);
            rear = front;
            Size++;
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
            Size++;
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
        Size--;

        return tailNode.Value;
    }

    public T RemoveFromFront()
    {
        LinkNode<T>? headNode = front
            ?? throw new Exception("error. cannot remove from front. deque is empty");

        headNode.Next!.Previous = null;
        front = headNode.Next;
        headNode.Next = null;
        Size--;

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
