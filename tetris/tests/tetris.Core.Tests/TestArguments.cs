using tetris.Core.Abstractions;
using tetris.Core.Shared;
using tetris.Core.Validators;

namespace tetris.Core.Tests;

using static tetris.Core.Helpers.ChainingHelper;

public class TestArguments
{
    [Theory]
    [InlineData("--difficulty 0")]
    [InlineData("--difficulty 1")]
    [InlineData("--difficulty 2")]
    [InlineData("-d 0")]
    [InlineData("-d 1")]
    [InlineData("-d 2")]
    public void Should_Test_Validator_Result(string arguments)
    {
        // Arrange
        string[] argumentList = arguments.Split(' ');

        // Act
        Result<bool> validatorResult = GetValidator(argumentList, out IValidate validator);

        // Assert
        Assert.True(validatorResult.Data);
        Assert.Null(validatorResult.Errors);
        Assert.NotNull(validator);
        Assert.IsType<DifficultyValidator>(validator);
    }
}
