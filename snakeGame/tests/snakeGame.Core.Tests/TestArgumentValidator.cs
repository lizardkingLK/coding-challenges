namespace snakeGame.Core.Tests;

using static snakeGame.Core.Helpers.ChainingHelper;

using snakeGame.Core.Shared;
using snakeGame.Core.Abstractions;

[Collection("snakeGame.Core.Tests")]
public class TestArgumentValidator
{
    [Fact]
    public void Should_Test_For_Arguments_Validation_No_Arguments()
    {
        // Act
        Result<bool> validatorResult = GetValidator([], 0, out _, out IValidate? validator);

        // Assert
        Assert.True(validatorResult.Data);
        Assert.NotNull(validator);
    }

    [Theory]
    [InlineData("-h")]
    [InlineData("--height")]
    [InlineData("-w", "4", "-w")]
    [InlineData("--width", "10", "--width")]
    public void Should_Test_For_Arguments_Validation_Incomplete_Arguments(params string[] arguments)
    {
        // Act
        Result<bool> validatorResult = GetValidator(arguments, arguments.Length, out _, out IValidate? validator);

        // Assert
        Assert.False(validatorResult.Data);
        Assert.Null(validator);
    }

    [Theory]
    [InlineData("-h", "20", "-h", "30")]
    [InlineData("--height", "20", "--height", "30")]
    [InlineData("-w", "4", "-w", "5")]
    [InlineData("--width", "10", "--width", "20")]
    public void Should_Test_For_Arguments_Validation_Duplicate_Arguments(params string[] arguments)
    {
        // Act
        Result<bool> validatorResult = GetValidator(arguments, arguments.Length, out _, out IValidate? validator);

        // Assert
        Assert.False(validatorResult.Data);
        Assert.Null(validator);
    }

    [Theory]
    [InlineData("-h", "20", "-w", "10")]
    [InlineData("--height", "20", "--width", "10")]
    [InlineData("-w", "10", "-h", "20")]
    [InlineData("--width", "10", "--height", "20")]
    [InlineData("-o", "0", "-gm", "0", "-d", "0")]
    [InlineData("--output", "1", "--game-mode", "1", "--difficulty", "0")]
    public void Should_Test_For_Arguments_Validation_Valid_Arguments(params string[] arguments)
    {
        // Act
        Result<bool> validatorResult = GetValidator(arguments, arguments.Length, out _, out IValidate? validator);

        // Assert
        Assert.True(validatorResult.Data);
        Assert.NotNull(validator);
    }
}