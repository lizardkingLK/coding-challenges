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
            HashMap<int, int> map = new(capacity);
        }

        // Act
        ApplicationException exception = Assert.Throws<ApplicationException>(act);

        // Assert
        Assert.Equal(ErrorHashMapInvalidCapacity, exception.Message);
    }
}