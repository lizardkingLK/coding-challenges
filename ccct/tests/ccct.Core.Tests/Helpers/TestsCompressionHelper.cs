using ccct.Core.Library.NonLinear.HashMaps;
using ccct.Core.State.Common;
using static ccct.Core.Helpers.CompressionHelper;
using static ccct.Core.Tests.Shared.Utility;
using static ccct.Core.Tests.Shared.Values;

namespace ccct.Core.Tests.Helpers;

public class CompressionHelper
{
    public class TestsDynamicallyAllocatedArray
    {
        [Fact]
        public void Should_Test_For_Frequency_Counts_Custom()
        {
            // Arrange
            string filePath = GetAbsolutePath("Data/Custom/input.txt");

            // Act
            Result<HashMap<char, long>> result = TrackFrequency(filePath);

            // Assert
            Assert.False(result.HasErrors);
            HashMap<char, long> data = result.Data;
            foreach ((char Character, long Count) in CustomInput)
            {
                Assert.Equal(Count, data[Character]);
            }
        }
    
        [Fact]
        public void Should_Test_For_Frequency_Counts_Official()
        {
            // Arrange
            string filePath = GetAbsolutePath("Data/Official/test.txt");

            // Act
            Result<HashMap<char, long>> result = TrackFrequency(filePath);

            // Assert
            Assert.False(result.HasErrors);
            HashMap<char, long> data = result.Data;
            foreach ((char Character, long Count) in OfficialInput)
            {
                Assert.Equal(Count, data[Character]);
            }
        }
    }
}