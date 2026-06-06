using SortTool.Core.Abstractions;

namespace SortTool.Core;

public class BubbleSort<T> : ISort<T> where T : IComparable<T>
{
    public void Sort(IList<T> values)
    {
        bool hasSwaps;
        for (int i = 0; i < values.Count; i++)
        {
            hasSwaps = false;
            for (int j = 0; j < values.Count - i - 1; j++)
            {
                if (values[j].CompareTo(values[j + 1]) > 0)
                {
                    (values[j + 1], values[j]) = (values[j], values[j + 1]);
                    hasSwaps = true;
                }
            }

            if (!hasSwaps)
            {
                break;
            }
        }
    }
}
