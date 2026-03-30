using ccct.Core.Library.Linear.Arrays;
using static ccct.Core.Shared.Errors;

namespace ccct.Core.Tests.Library.Linear.Arrays;

public class TestsDynamicallyAllocatedArray
{
    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-1)]
    [InlineData(-100)]
    [InlineData(int.MaxValue)]
    public void Should_Initialize_DynamicallyAllocatedArray_Exceptions(int capacity)
    {
        // Arrange
        void act()
        {
            DynamicallyAllocatedArray<int> array = new(capacity);
        }

        // Act
        ApplicationException exception = Assert.Throws<ApplicationException>(act);

        // Assert
        Assert.Equal(ErrorDynamicallyAllocatedArrayInvalidCapacity, exception.Message);
    }

    [Fact]
    public void Should_Initialize_DynamicallyAllocatedArray_Exceptions_Special()
    {
        // Arrange
        static void act()
        {
            DynamicallyAllocatedArray<int> array = new(Array.MaxLength);
        }

        // Act
        ApplicationException exception = Assert.Throws<ApplicationException>(act);

        // Assert
        Assert.Equal(ErrorDynamicallyAllocatedArrayInvalidCapacity, exception.Message);
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, 1)]
    [InlineData(1, 2)]
    public void Should_Add_DynamicallyAllocatedArray(int value1, int value2)
    {
        // Arrange
        DynamicallyAllocatedArray<int> array =
        [
            // Act
            value1,
            value2,
        ];

        // Assert
        Assert.Equal(value1, array[0]);
        Assert.Equal(value2, array[1]);
        Assert.Equal(2, array.Size);
        Assert.Equal(4, array.Capacity);
    }

    [Fact]
    public void Should_Initialize_DynamicallyAllocatedArray_Types()
    {
        // Arrange
        DynamicallyAllocatedArray<bool> array1 = [true, false, true];
        DynamicallyAllocatedArray<int> array2 = [-1, 0, 1];
        DynamicallyAllocatedArray<string> array3 = ["Hello", " ", "World", "!"];
        DynamicallyAllocatedArray<float> array4 = [1.2f, 2.3f, 3.5f];
        DynamicallyAllocatedArray<object> array5 = ["h", 'a', 1, 1f, true];
        DynamicallyAllocatedArray<dynamic> array6 = ["h", 'a', 1, 1f, true];

        // Assert
        Assert.True(array1[0]);
        Assert.False(array1[1]);
        Assert.True(array1[2]);

        Assert.Equal(-1, array2[0]);
        Assert.Equal(0, array2[1]);
        Assert.Equal(1, array2[2]);

        Assert.Equal("Hello", array3[0]);
        Assert.Equal(" ", array3[1]);
        Assert.Equal("World", array3[2]);
        Assert.Equal("!", array3[3]);

        Assert.Equal(1.2f, array4[0]);
        Assert.Equal(2.3f, array4[1]);
        Assert.Equal(3.5f, array4[2]);

        Assert.Equal("h", array5[0]);
        Assert.Equal('a', array5[1]);
        Assert.Equal(1, array5[2]);
        Assert.Equal(1f, array5[3]);
        Assert.Equal(true, array5[4]);

        Assert.Equal("h", array6[0]);
        Assert.Equal('a', array6[1]);
        Assert.Equal(1, array6[2]);
        Assert.Equal(1f, array6[3]);
        Assert.Equal(true, array6[4]);
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, 1)]
    [InlineData(1, 2)]
    public void Should_GetValues_DynamicallyAllocatedArray(int value1, int value2)
    {
        // Arrange
        int[] values = [value1, value2];

        DynamicallyAllocatedArray<int> array =
        [
            // Act
            value1,
            value2,
        ];

        // Assert
        for (int i = 0; i < 2; i++)
        {
            Assert.Equal(values[i], array[i]);
        }
    }
}