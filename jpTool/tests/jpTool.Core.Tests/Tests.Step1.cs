namespace jpTool.Core.Tests;

public class TestsStep1
{
    [Theory]
    [InlineData(@"data\step1\invalid.json", false)]
    [InlineData(@"data\step1\valid.json", true)]
    [InlineData(@"data\step2\invalid.json", false)]
    [InlineData(@"data\step2\invalid2.json", false)]
    public void Should_Test_For_Valid_Json_In_Step_1(string filePath, bool expectedResult)
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
