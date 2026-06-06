using SortTool.Core;

namespace SortTool.Program;

public class Program
{
    record Person : IComparable<Person>
    {
        public string? Name { get; set; }
        public int Age { get; set; }

        public int CompareTo(Person? other) => Age.CompareTo(other?.Age ?? 0);
    }

    public static void Main()
    {
        // int[] values = [9, 1, 2, 7, 9, -6, 5, 6, 0, 9, 5, 11, 6];
        int[] values = [2, 7, 6, 5, 4, 9, 8, 7, 1, 10, -20, 30];
        // int[] values = [64, 34, 25, 12, 22, 11, 90];
        // int[] values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
        // int[] values = [100, 90, 80, 70, 60, 50, 40, 30, 20, 10];
        // int[] values = [5, 2, 8, 5, 1, 9, 2, 5, 3, 8];
        // int[] values = [42, 17];
        // int[] values = [100];
        // int[] values = [];
        // int[] values = [3, 3, 3, 3, 3];
        // int[] values = [3, 3, 3, 3, 3];
        // int[] values = [.. Enumerable.Range(1, 1001).Select(_ => Random.Shared.Next(-1000, 1000))];
        // int[] values = [.. Enumerable.Range(1, 1001).Select(_ => Random.Shared.Next(-1000, 1000)).OrderDescending()];
        // string[] values = ["zebra", "apple", "monkey", "banana", "cat", "dog"];
        // List<Person> values =
        // [
        //     new() { Name = "Bob", Age = 25 },
        //     new() { Name = "Alice", Age = 30 },
        //     new() { Name = "Charlie", Age = 20 },
        //     new() { Name = "David", Age = 25 }
        // ];

        // BubbleSort<int> sorter = new();
        // BubbleSort<string> sorter = new();
        // BubbleSort<Person> sorter = new();

        // SelectionSort<int> sorter = new();
        // SelectionSort<string> sorter = new();
        // SelectionSort<Person> sorter = new();

        // HeapSort<int> sorter = new();
        // HeapSort<string> sorter = new();
        // HeapSort<Person> sorter = new();

        QuickSort<int> sorter = new();
        // QuickSort<string> sorter = new();
        // QuickSort<Person> sorter = new();

        sorter.Sort(values);
        Console.WriteLine(string.Join(' ', values));
        // Console.WriteLine(string.Join('\n', values));
    }
}
