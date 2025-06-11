namespace jpTool.Core.Tests;

using static Utility;

[Collection("jpTool.Core.Tests")]
public class TestsOfficialJson
{
    [Theory]
    [InlineData(@"fail1.json", false)]
    [InlineData(@"fail10.json", false)]
    [InlineData(@"fail11.json", false)]
    [InlineData(@"fail12.json", false)]
    [InlineData(@"fail13.json", false)]
    [InlineData(@"fail14.json", false)]
    [InlineData(@"fail15.json", false)]
    [InlineData(@"fail16.json", false)]
    [InlineData(@"fail17.json", false)]
    [InlineData(@"fail18.json", true)]
    [InlineData(@"fail19.json", false)]
    [InlineData(@"fail2.json", false)]
    [InlineData(@"fail20.json", false)]
    [InlineData(@"fail21.json", false)]
    [InlineData(@"fail22.json", false)]
    [InlineData(@"fail23.json", false)]
    [InlineData(@"fail24.json", false)]
    [InlineData(@"fail25.json", false)]
    [InlineData(@"fail26.json", false)]
    [InlineData(@"fail27.json", false)]
    [InlineData(@"fail28.json", false)]
    [InlineData(@"fail29.json", false)]
    [InlineData(@"fail3.json", false)]
    [InlineData(@"fail30.json", false)]
    [InlineData(@"fail31.json", false)]
    [InlineData(@"fail32.json", false)]
    [InlineData(@"fail33.json", false)]
    [InlineData(@"fail4.json", false)]
    [InlineData(@"fail5.json", false)]
    [InlineData(@"fail6.json", false)]
    [InlineData(@"fail7.json", false)]
    [InlineData(@"fail8.json", false)]
    [InlineData(@"fail9.json", false)]
    [InlineData(@"pass1.json", true)]
    [InlineData(@"pass2.json", true)]
    [InlineData(@"pass3.json", true)]
    public void Should_Test_For_Given_Json_Data(string filePath, bool expectedResult)
    {
        filePath = @$"data\official\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }
}