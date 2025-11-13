using tetris.Core;

namespace tetris.Program;

// public record LinkNode<T>
// {
//     public LinkNode<T>? Previous { get; set; }
//     public T Value { get; set; }
//     public LinkNode<T>? Next { get; set; }

//     public LinkNode(T value) : this(null, value, null)
//     {
//     }

//     public LinkNode(LinkNode<T>? previous, T value, LinkNode<T>? next)
//     {
//         Previous = previous;
//         Value = value;
//         Next = next;
//     }
// }

// public class Deque<T>
// {
//     private LinkNode<T>? front;
//     private LinkNode<T>? rear;
//     public int Size { get; set; }

//     public Deque()
//     {
//     }

//     public Deque(T head)
//     {
//         front = new(null, head, null);
//         rear = front;
//         Size++;
//     }

//     public void InsertToRear(T item)
//     {
//         LinkNode<T>? tailNode = rear;
//         if (tailNode == null)
//         {
//             rear = new LinkNode<T>(null, item, null);
//             Size++;
//             if (front == null)
//             {
//                 return;
//             }

//             LinkNode<T>? current = front;
//             LinkNode<T>? previous = null;
//             while (current != null)
//             {
//                 previous = current;
//                 current = current.Next;
//             }

//             previous!.Next = rear;
//             rear.Previous = previous;

//             return;
//         }

//         tailNode = new LinkNode<T>(null, item, null);
//         rear!.Next = tailNode;
//         tailNode.Previous = rear;
//         rear = tailNode;
//         Size++;
//     }

//     public void InsertToFront(T item)
//     {
//         LinkNode<T>? headNode = front;
//         if (headNode == null)
//         {
//             front = new LinkNode<T>(null, item, null);
//             Size++;

//             if (rear == null)
//             {
//                 return;
//             }

//             LinkNode<T>? current = rear;
//             LinkNode<T>? next = null;
//             while (current != null)
//             {
//                 next = current;
//                 current = current.Next;
//             }

//             next!.Previous = front;
//             front.Next = next;

//             return;
//         }

//         headNode = new LinkNode<T>(null, item, null);
//         front!.Previous = headNode;
//         headNode.Next = front;
//         front = headNode;
//         Size++;
//     }

//     public void DisplayFrontToRear()
//     {
//         LinkNode<T>? headNode = front;
//         while (headNode != null)
//         {
//             Console.Write($"{headNode.Value} ");
//             headNode = headNode.Next;
//         }

//         Console.WriteLine();
//     }

//     public T RemoveFromRear()
//     {
//         LinkNode<T> tailNode = rear
//             ?? throw new Exception("error. cannot remove from rear. deque is empty");
//         if (tailNode.Previous != null)
//         {
//             tailNode.Previous.Next = null;
//         }

//         rear = tailNode.Previous;
//         tailNode.Previous = null;
//         Size--;

//         return tailNode.Value;
//     }

//     public T RemoveFromFront()
//     {
//         LinkNode<T> headNode = front
//             ?? throw new Exception("error. cannot remove from front. deque is empty");
//         if (headNode.Next != null)
//         {
//             headNode.Next.Previous = null;
//         }

//         front = headNode.Next;
//         headNode.Next = null;
//         Size--;

//         return headNode.Value;
//     }

//     public void DisplayRearToFront()
//     {
//         LinkNode<T>? tailNode = rear;
//         while (tailNode != null)
//         {
//             Console.Write($"{tailNode.Value} ");
//             tailNode = tailNode.Previous;
//         }

//         Console.WriteLine();
//     }

//     public T? SearchValue(int index)
//     {
//         LinkNode<T>? currentNode = front;
//         int i = 0;
//         while (currentNode != null && i <= index)
//         {
//             currentNode = currentNode.Next;
//             i++;
//         }

//         if (currentNode == null)
//         {
//             return default;
//         }

//         return currentNode.Value;
//     }

//     public T SeekRear()
//     {
//         LinkNode<T> tailNode = rear
//             ?? throw new Exception("error. cannot seek at rear. deque is empty");

//         return tailNode.Value;
//     }

//     public T SeekFront()
//     {
//         LinkNode<T> headNode = front 
//             ?? throw new Exception("error. cannot seek at front. deque is empty");

//         return headNode.Value;
//     }
// }

// public class Program
// {
//     public static void Main()
//     {
//         var d1 = new Deque<int>();
//         d1.InsertToFront(0);
//         d1.InsertToFront(1);
//         d1.InsertToRear(2);
//         Console.WriteLine(d1.RemoveFromFront());
//         Console.WriteLine(d1.RemoveFromFront());
//         Console.WriteLine(d1.RemoveFromFront());
//     }
// }

class Program
{
    static void Main(string[] args) => Tetris.Play(args);
}
