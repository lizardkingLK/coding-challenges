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
}