using ccct.Core.Library.NonLinear.HashMaps;
using static ccct.Core.Shared.Errors;

namespace ccct.Core.Tests.Library.NonLinear.HashMaps;

public class TestsHashMap
{
    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-1)]
    [InlineData(-100)]
    [InlineData(int.MaxValue)]
    public void Should_Initialize_HashMap_Exceptions(int capacity)
    {
        // Arrange
        void act()
        {
            HashMap<int, int> hashMap = new(capacity);
        }

        // Act
        ApplicationException exception = Assert.Throws<ApplicationException>(act);

        // Assert
        Assert.Equal(ErrorHashMapInvalidCapacity, exception.Message);
    }

    [Fact]
    public void Should_Initialize_HashMap_Exceptions_Special()
    {
        // Arrange
        static void act()
        {
            HashMap<int, int> hashMap = new(Array.MaxLength);
        }

        // Act
        ApplicationException exception = Assert.Throws<ApplicationException>(act);

        // Assert
        Assert.Equal(ErrorHashMapInvalidCapacity, exception.Message);
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, 1)]
    [InlineData(1, 2)]
    public void Should_Add_HashMap(int value1, int value2)
    {
        // Arrange
        HashMap<int, int> hashMap = [];

        // Act
        hashMap.Add(value1, value2);

        // Assert
        Assert.Equal(value2, hashMap[value1]);
        Assert.Equal(1, hashMap.Size);
        Assert.Equal(2, hashMap.Capacity);
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, 1)]
    [InlineData(1, 2)]
    public void Should_Add_HashMap_Exceptions(int value1, int value2)
    {
        // Arrange
        HashMap<int, int> hashMap = [];
        hashMap.Add(value1, value2);

        // Act
        void act()
        {
            hashMap.Add(value1, value2);
        }

        // Assert
        ApplicationException exception = Assert.Throws<ApplicationException>(act);
        Assert.Equal(ErrorHashMapKeyAlreadyExist, exception.Message);
    }

    [Theory]
    [InlineData(-1, 0, 5)]
    [InlineData(0, 1, 6)]
    [InlineData(1, 2, 7)]
    public void Should_Update_HashMap(int value1, int value2, int value3)
    {
        // Arrange
        HashMap<int, int> hashMap = [];
        hashMap.Add(value1, value2);

        // Act
        int previous = hashMap[value1];
        hashMap[value1] = value3;
        int current = hashMap[value1];

        // Assert
        Assert.Equal(value2, previous);
        Assert.Equal(1, hashMap.Size);
        Assert.Equal(2, hashMap.Capacity);
        Assert.Equal(value3, current);
    }
}