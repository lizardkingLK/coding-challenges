namespace jpTool.Core.Tests;

using static jpTool.Core.Utility;
using static jpTool.Core.Constants;
using static Utility;

[Collection("jpTool.Core.Tests")]
public class TestsUtilityMethods
{
    [Fact]
    public void Should_Test_For_Argument_Validation_Method_Count_Invalid()
    {
        // Act
        Result<bool> actualResult = IsValidArguments([]);

        // Assert
        Assert.False(actualResult.Data);
        Assert.Equal(ERROR_NOT_ENOUGH_ARGUMENTS, actualResult.Errors);
    }

    [Theory]
    [InlineData("valid_path_to_a_json_file")]
    [InlineData("invalid_path_to_a_json_file")]
    public void Should_Test_For_Argument_Validation_Method_Count_Valid(params string[] arguments)
    {
        // Act
        Result<bool> actualResult = IsValidArguments(arguments);

        // Assert
        Assert.True(actualResult.Data);
        Assert.Null(actualResult.Errors);
    }

    [Theory]
    [InlineData("invalid_path_to_a_json_file")]
    [InlineData("another_invalid_path_to_a_json_file")]
    public void Should_Test_For_File_Path_Validation_InValid(string filePath)
    {
        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        Result<string> actualResult = ProcessFilePath(absoluteFilePath);

        // Assert
        Assert.Equal(ERROR_INVALID_FILE_PATH, actualResult.Errors);
    }

    [Theory]
    [InlineData("array.invalid.json")]
    [InlineData("array.valid.json")]
    public void Should_Test_For_File_Path_Validation_Valid(string filePath)
    {
        filePath = @$"data\custom\array\array\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        Result<string> actualResult = ProcessFilePath(absoluteFilePath);

        // Assert
        Assert.NotNull(actualResult.Data);
        Assert.Null(actualResult.Errors);
    }
}