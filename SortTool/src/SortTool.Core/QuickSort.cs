using SortTool.Core.Abstractions;

namespace SortTool.Core;

public class QuickSort<T> : ISort<T> where T : IComparable<T>
{
    public void Sort(IList<T> values)
    {
        Sort(values, start: 0, end: values.Count - 1);
    }

    private static void Sort(IList<T> values, int start, int end)
    {
        if (start < end)
        {
            int pivot = Partition(values, start, end);
            Sort(values, start, pivot - 1);
            Sort(values, pivot + 1, end);
        }
    }

    private static int Partition(IList<T> values, int start, int end)
    {
        int pivot = end;
        int i = start - 1;
        for (int j = start; j <= end; j++)
        {
            if (values[j].CompareTo(values[pivot]) < 0)
            {
                (values[i + 1], values[j]) = (values[j], values[i + 1]);
                i++;
            }
        }

        (values[i + 1], values[pivot]) = (values[pivot], values[i + 1]);

        return i + 1;
    }
}