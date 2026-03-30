using ccct.Core.Library.Linear.Arrays;
using ccct.Core.Library.Linear.Lists;
using static ccct.Core.Shared.Errors;

namespace ccct.Core.Tests.Library.Linear.Lists;

public class TestsDoublyLinkedList
{
    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(true, false, true)]
    [InlineData("Hello", " ", "World", "!")]
    [InlineData(1f, 2f, 3f, 1.2f, 2.3f, 3.4f)]
    public void Should_AddToFront_DoublyLinkedList(params object[] values)
    {
        // Arrange
        DoublyLinkedList<object> list = new();

        // Act
        foreach (object value in values)
        {
            list.AddToFront(value);
        }

        // Assert
        DynamicallyAllocatedArray<object> val = [.. list.Values];
        int n = val.Size;
        for (int i = 0; i < values.Length; i++)
        {
            Assert.Equal(values[i], val[n - 1 - i]);
        }
    }

    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(true, false, true)]
    [InlineData("Hello", " ", "World", "!")]
    [InlineData(1f, 2f, 3f, 1.2f, 2.3f, 3.4f)]
    public void Should_AddToRear_DoublyLinkedList(params object[] values)
    {
        // Arrange
        DoublyLinkedList<object> list = new();

        // Act
        foreach (object value in values)
        {
            list.AddToRear(value);
        }

        // Assert
        DynamicallyAllocatedArray<object> val = [.. list.Values];
        for (int i = 0; i < values.Length; i++)
        {
            Assert.Equal(values[i], val[i]);
        }
    }

    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(true, false, true)]
    [InlineData("Hello", " ", "World", "!")]
    [InlineData(1f, 2f, 3f, 1.2f, 2.3f, 3.4f)]
    public void Should_RemoveFromFront_DoublyLinkedList(params object[] values)
    {
        // Arrange
        DoublyLinkedList<object> list = new();
        foreach (object value in values)
        {
            list.AddToFront(value);
        }

        // Act
        object removedValue = list.RemoveFromFront().Value;

        // Assert
        Assert.Equal(values[^1], removedValue);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(true)]
    [InlineData("Hello")]
    [InlineData(1f)]
    public void Should_RemoveFromFront_DoublyLinkedList_Exception(params object[] values)
    {
        // Arrange
        DoublyLinkedList<object> list = new();
        foreach (object value in values)
        {
            list.AddToFront(value);
        }

        object removedValue = list.RemoveFromFront().Value;

        // Act
        void act()
        {
            removedValue = list.RemoveFromFront().Value;
        }

        ApplicationException exception = Assert.Throws<ApplicationException>(act);

        // Assert
        Assert.Equal(ErrorDoublyLinkedListCannotRemoveFromFront, exception.Message);
    }

    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(true, false, true)]
    [InlineData("Hello", " ", "World", "!")]
    [InlineData(1f, 2f, 3f, 1.2f, 2.3f, 3.4f)]
    public void Should_RemoveFromRear_DoublyLinkedList(params object[] values)
    {
        // Arrange
        DoublyLinkedList<object> list = new();
        foreach (object value in values)
        {
            list.AddToRear(value);
        }

        // Act
        object removedValue = list.RemoveFromRear().Value;

        // Assert
        Assert.Equal(values[^1], removedValue);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(true)]
    [InlineData("Hello")]
    [InlineData(1f)]
    public void Should_RemoveFromRear_DoublyLinkedList_Exception(params object[] values)
    {
        // Arrange
        DoublyLinkedList<object> list = new();
        foreach (object value in values)
        {
            list.AddToRear(value);
        }

        object removedValue = list.RemoveFromRear().Value;

        // Act
        void act()
        {
            removedValue = list.RemoveFromRear().Value;
        }

        ApplicationException exception = Assert.Throws<ApplicationException>(act);

        // Assert
        Assert.Equal(ErrorDoublyLinkedListCannotRemoveFromRear, exception.Message);
    }

    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(true, false, true)]
    [InlineData("Hello", " ", "World", "!")]
    [InlineData(1f, 2f, 3f, 1.2f, 2.3f, 3.4f)]
    public void Should_GetValues_DoublyLinkedList(params object[] values)
    {
        // Arrange
        DoublyLinkedList<object> list = new();
        foreach (object value in values)
        {
            list.AddToRear(value);
        }

        // Act
        DynamicallyAllocatedArray<object> outputs = [.. list.Values];

        // Assert
        for (int i = 0; i < outputs.Size; i++)
        {
            Assert.Equal(values[i], outputs[i]);
        }
    }

    [Theory]
    [InlineData(1, true, 1, 2, 3)]
    [InlineData(false, true, true, false, true)]
    [InlineData("!", true, "Hello", " ", "World", "!")]
    [InlineData(2.3f, true, 1f, 2f, 3f, 1.2f, 2.3f, 3.4f)]
    [InlineData(-1, false, 1, 2, 3)]
    [InlineData(false, false, true, true, true)]
    [InlineData("Bye", false, "Hello", " ", "World", "!")]
    [InlineData(-2.4f, false, 1f, 2f, 3f, 1.2f, 2.3f, 3.4f)]
    [InlineData(null, false, 1, 2, 3)]
    [InlineData(null, false, true, true, true)]
    [InlineData(null, false, "Hello", " ", "World", "!")]
    [InlineData(null, false, 1f, 2f, 3f, 1.2f, 2.3f, 3.4f)]
    public void Should_Exists_DoublyLinkedList(object? target, bool expected, params object[] values)
    {
        // Arrange
        DoublyLinkedList<object?> list = new();

        // Act
        foreach (object value in values)
        {
            list.AddToRear(value);
        }

        // Assert
        Assert.Equal(expected, list.Exists(item => item is null || item.Equals(target)));
    }

    [Theory]
    [InlineData(1, true, 1, 2, 3)]
    [InlineData(false, true, true, false, true)]
    [InlineData("!", true, "Hello", " ", "World", "!")]
    [InlineData(2.3f, true, 1f, 2f, 3f, 1.2f, 2.3f, 3.4f)]
    [InlineData(null, false, 1, 2, 3)]
    [InlineData(null, false, true, true, true)]
    [InlineData(null, false, "Hello", " ", "World", "!")]
    [InlineData(null, false, 1f, 2f, 3f, 1.2f, 2.3f, 3.4f)]
    public void Should_TryGetValue_DoublyLinkedList(object? target, bool expected, params object[] values)
    {
        // Arrange
        DoublyLinkedList<object?> list = new();

        // Act
        foreach (object value in values)
        {
            list.AddToRear(value);
        }

        bool isAvailable = list.TryGetValue(
            item => item is null || item.Equals(target),
            out object? got);

        // Assert
        Assert.Equal(expected, isAvailable);
        Assert.Equal(target, got);
    }

    [Theory]
    [InlineData(1, 10, 1, 2, 3)]
    [InlineData(false, true, true, false, true)]
    // [InlineData("!", "#", "Hello", " ", "World", "!")]
    // [InlineData(2.3f, 6.7f, 1f, 2f, 3f, 1.2f, 2.3f, 3.4f)]
    public void Should_Update_DoublyLinkedList(object? target, object? newValue, params object[] values)
    {
        // Arrange
        DoublyLinkedList<object?> list = new();

        // Act
        foreach (object value in values)
        {
            list.AddToRear(value);
        }

        list.Update((item) => item != null && item.Equals(target), newValue);

        bool isAvailable = list.TryGetValue(
            item => item is not null && item.Equals(newValue),
            out object? got);

        // Assert
        Assert.True(isAvailable);
        Assert.Equal(newValue, got);
    }
}