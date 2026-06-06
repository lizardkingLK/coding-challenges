using SortTool.Core.Abstractions;

namespace SortTool.Core;

public class HeapSort<T> : ISort<T> where T : IComparable<T>
{
    public void Sort(IList<T> values)
    {
        HeapSort<T>.Heapify(values);
        HeapSort<T>.Arrange(values);
    }

    private static void Arrange(IList<T> values)
    {
        int length = values.Count;
        int i = 0;
        int upperbound = length - 1;
        while (i < length)
        {
            Swap(values, 0, upperbound);
            HeapifyDown(values, upperbound, 0);
            i++;
            upperbound--;
        }
    }

    private static void Heapify(IList<T> values)
    {
        int length = values.Count;
        int n = length / 2 - 1;
        for (int i = n; i >= 0; i--)
        {
            HeapSort<T>.HeapifyDown(values, length, i);
        }
    }

    private static void HeapifyDown(IList<T> values, int length, int index)
    {
        (int max, int left, int right) = (index, 2 * index + 1, 2 * index + 2);
        if (left < length && values[max].CompareTo(values[left]) < 0)
        {
            max = left;
        }

        if (right < length && values[max].CompareTo(values[right]) < 0)
        {
            max = right;
        }

        if (max == index)
        {
            return;
        }

        HeapSort<T>.Swap(values, max, index);

        HeapSort<T>.HeapifyDown(values, length, max);
    }

    private static void Swap(IList<T> values, int i, int j)
    {
        (values[i], values[j]) = (values[j], values[i]);
    }
}