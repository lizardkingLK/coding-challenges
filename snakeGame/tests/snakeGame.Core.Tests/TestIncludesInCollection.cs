using static snakeGame.Core.Shared.Values;
using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Shared.Utility;

namespace snakeGame.Core.Tests;

[Collection("snakeGame.Core.Tests")]
public class TestArguments
{
    [Theory]
    [InlineData(FlagHeight, true)]
    [InlineData(FlagHeightPrefixed, true)]
    [InlineData(FlagWidth, false)]
    [InlineData(FlagWidthPrefixed, false)]
    public void Should_Test_For_Includes_Value_In_The_Collection_Height_Flags_Collection(string flagValue, bool expectedResult)
    {
        // Act
        bool actualResult = IncludesInCollection(flagValue, heightFlags);

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [InlineData(FlagWidth, true)]
    [InlineData(FlagWidthPrefixed, true)]
    [InlineData(FlagHeight, false)]
    [InlineData(FlagHeightPrefixed, false)]
    public void Should_Test_For_Includes_Value_In_The_Collection_Width_Flags_Collection(string flagValue, bool expectedResult)
    {
        // Act
        bool actualResult = IncludesInCollection(flagValue, widthFlags);

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }
}
