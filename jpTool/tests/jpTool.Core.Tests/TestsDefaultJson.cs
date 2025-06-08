namespace jpTool.Core.Tests;

using static Utility;

public class TestsDefaultJson
{
    [Theory]
    [InlineData(@"data\default\step1\invalid.json", false)]
    [InlineData(@"data\default\step1\valid.json", true)]
    public void Should_Test_For_Valid_Json_In_Step_1(string filePath, bool expectedResult)
    {
        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"data\default\step2\invalid.json", false)]
    [InlineData(@"data\default\step2\invalid2.json", false)]
    [InlineData(@"data\default\step2\valid.json", true)]
    [InlineData(@"data\default\step2\valid2.json", true)]
    public void Should_Test_For_Valid_Json_In_Step_2(string filePath, bool expectedResult)
    {
        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"data\default\step3\invalid.json", false)]
    [InlineData(@"data\default\step3\valid.json", true)]
    public void Should_Test_For_Valid_Json_In_Step_3(string filePath, bool expectedResult)
    {
        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"data\default\step4\invalid.json", false)]
    [InlineData(@"data\default\step4\valid.json", true)]
    [InlineData(@"data\default\step4\valid2.json", true)]
    public void Should_Test_For_Valid_Json_In_Step_4(string filePath, bool expectedResult)
    {
        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }
}
