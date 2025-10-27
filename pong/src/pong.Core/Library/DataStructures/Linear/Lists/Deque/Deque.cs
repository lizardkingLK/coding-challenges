using pong.Core.Library.DataStructures.Linear.Lists.DoublyLinkedList;

namespace pong.Core.Library.DataStructures.Linear.Lists.Deque;

public class Deque<T>
{
    private DoublyLinkedList<T> _list = new();

    public void Enqueue(T value)
    {
        _list.AddToFront(value);
    }

    public T Dequeue()
    {
        _list   
    }
}