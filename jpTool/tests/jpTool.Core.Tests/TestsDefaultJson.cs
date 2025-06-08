namespace jpTool.Core.Tests;

public class TestsDefaultJson
{
    [Theory]
    [InlineData(@"data\default\step1\invalid.json", false)]
    [InlineData(@"data\default\step1\valid.json", true)]
    [InlineData(@"data\default\step2\invalid.json", false)]
    [InlineData(@"data\default\step2\invalid1.json", false)]
    [InlineData(@"data\default\step2\invalid2.json", false)]
    [InlineData(@"data\default\step2\valid.json", true)]
    [InlineData(@"data\default\step2\valid2.json", true)]
    public void Should_Test_For_Valid_Json_In_Steps(string filePath, bool expectedResult)
    {
        CommonTestWrapper(filePath, expectedResult);
    }

    private static void CommonTestWrapper(string filePath, bool expectedResult)
    {
        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    private static string GetAbsoluteFilePath(string filePath)
    {
        return Path.Combine(Directory.GetCurrentDirectory(), filePath);
    }
}
