namespace snakeGame.Core.Tests;

using static snakeGame.Core.Helpers.ChainingHelper;
using static snakeGame.Core.Shared.Constants;

using snakeGame.Core.Shared;
using snakeGame.Core.Abstractions;
using snakeGame.Core.State;

[Collection("snakeGame.Core.Tests")]
public class TestArgumentValidation
{
    [Theory]
    [InlineData("-w", "width123")]
    [InlineData("--width", "-345")]
    [InlineData("-w", "31_000")]
    [InlineData("--height", "how456")]
    [InlineData("-h", "-567")]
    [InlineData("-h", "45_000")]
    [InlineData("-o", "-1")]
    [InlineData("--output", "100")]
    [InlineData("-gm", "200")]
    [InlineData("--difficulty", "-400")]
    public void Should_Test_For_Arguments_Validation_Invalid_Arguments(params string[] args)
    {
        // Arrange
        Result<bool> validatorResult = GetValidator(args, args.Length, out Arguments arguments, out IValidate? validator);

        // Act
        Result<bool> validationResult = validator!.Validate(ref arguments);

        // Assert
        Assert.True(validatorResult.Data);
        Assert.False(validationResult.Data);
        Assert.Equal(ERROR_INVALID_ARGUMENTS, validationResult.Error);
    }
}