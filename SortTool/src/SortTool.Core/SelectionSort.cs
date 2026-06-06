using SortTool.Core.Abstractions;

namespace SortTool.Core;

public class SelectionSort<T> : ISort<T> where T : IComparable<T>
{
    public void Sort(IList<T> values)
    {
        int length = values.Count;
        for (int i = 0; i < length; i++)
        {
            (int minimumIndex, int maximumIndex) = (i, i);
            for (int j = i + 1; j < length; j++)
            {
                if (values[minimumIndex].CompareTo(values[j]) == 1)
                {
                    minimumIndex = j;
                }

                if (values[maximumIndex].CompareTo(values[j]) == -1)
                {
                    maximumIndex = j;
                }
            }

            (values[i], values[minimumIndex]) = (values[minimumIndex], values[i]);
            if (i == maximumIndex)
            {
                continue;    
            }

            (values[length - 1], values[maximumIndex]) = (values[maximumIndex], values[--length]);
        }
    }
}