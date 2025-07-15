namespace snakeGame.Core.Tests;

using snakeGame.Core.Shared;

using static snakeGame.Core.Shared.Constants;
using static snakeGame.Core.Helpers.ArgumentHelper;
using snakeGame.Core.Enums;

[Collection("snakeGame.Core.Tests")]
public class TestValidateArguments
{
    [Fact]
    public void Should_Test_For_Arguments_Validation_No_Arguments()
    {
        // Act
        Result<(bool, int, int, OutputTypeEnum)> validateArgumentResult = ValidateArguments([], default, default);

        // Assert
        Assert.True(validateArgumentResult.Data.Item1);
        Assert.Equal(default, validateArgumentResult.Data.Item2);
        Assert.Equal(default, validateArgumentResult.Data.Item3);
    }

    [Theory]
    [InlineData("1")]
    [InlineData("1", "2", "3")]
    public void Should_Test_For_Arguments_Validation_Invalid_Argument_Counts(params string[] arguments)
    {
        // Act
        Result<(bool, int, int, OutputTypeEnum)> validateArgumentResult = ValidateArguments(arguments, default, default);

        // Assert
        Assert.False(validateArgumentResult.Data.Item1);
        Assert.Equal(-1, validateArgumentResult.Data.Item2);
        Assert.Equal(-1, validateArgumentResult.Data.Item3);
    }

    [Theory]
    [InlineData(FlagHeight, "non parsable int", false)]
    [InlineData(FlagHeight, "non parsable int but 1", false)]
    public void Should_Test_For_Arguments_Validation_Invalid_Arguments(string flagArgument, string flagValue, bool expectedResult)
    {
        // Act
        Result<(bool, int, int, OutputTypeEnum)> validateArgumentResult = ValidateArguments([flagArgument, flagValue], default, default);

        // Assert
        Assert.Equal(expectedResult, validateArgumentResult.Data.Item1);
    }

    [Theory]
    [InlineData("100", 100, "200", 200, 100, 200)]
    [InlineData("200", 200, "400", 400, 200, 400)]
    [InlineData("101", 100, "201", 200, 100, 200)]
    [InlineData("201", 200, "400", 400, 200, 400)]
    [InlineData("-1", 100, "201", 200, 100, 200)]
    [InlineData("5", 200, "400", 400, 200, 400)]
    [InlineData("101", 100, "-1", 200, 100, 200)]
    [InlineData("201", 200, "5", 400, 200, 400)]
    public void Should_Test_For_Arguments_Validation_Height(
        string heightValue,
        int maxHeight,
        string widthValue,
        int maxWidth,
        int expectedHeight,
        int expectedWidth)
    {
        // Arrange
        string[] args = [FlagHeightPrefixed, heightValue, FlagWidthPrefixed, widthValue];

        // Act
        Result<(bool, int, int, OutputTypeEnum)> validateArgumentResult = ValidateArguments(args, maxHeight, maxWidth);

        // Assert
        Assert.True(validateArgumentResult.Data.Item1);
        Assert.Equal(expectedHeight, validateArgumentResult.Data.Item2);
        Assert.Equal(expectedWidth, validateArgumentResult.Data.Item3);
    }
}