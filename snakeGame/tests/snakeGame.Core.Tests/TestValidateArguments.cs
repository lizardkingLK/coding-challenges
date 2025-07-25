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
        Result<(bool, int, int, OutputTypeEnum, GameModeEnum)> validateArgumentResult = ValidateArguments([]);

        // Assert
        Assert.True(validateArgumentResult.Data.Item1);
        Assert.Equal(default, validateArgumentResult.Data.Item2);
        Assert.Equal(default, validateArgumentResult.Data.Item3);
        Assert.Equal(default, validateArgumentResult.Data.Item4);
    }

    [Theory]
    [InlineData("1")]
    [InlineData("1", "2", "3")]
    public void Should_Test_For_Arguments_Validation_Invalid_Argument_Counts(params string[] arguments)
    {
        // Act
        Result<(bool, int, int, OutputTypeEnum, GameModeEnum)> validateArgumentResult = ValidateArguments(arguments);

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
        Result<(bool, int, int, OutputTypeEnum, GameModeEnum)> validateArgumentResult = ValidateArguments([flagArgument, flagValue]);

        // Assert
        Assert.Equal(expectedResult, validateArgumentResult.Data.Item1);
    }
}