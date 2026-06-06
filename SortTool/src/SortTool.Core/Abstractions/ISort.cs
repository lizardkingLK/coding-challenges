namespace SortTool.Core.Abstractions;

public interface ISort<T> where T : IComparable<T>
{
    public void Sort(IList<T> values);
}