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


    ///////////////////////////////////////////
    /// 
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
}