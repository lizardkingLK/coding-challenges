namespace wcTool.Core.Tests;

public class Test
{
    private static string GetAbsoluteFilePath(string relativeFilePath)
    {
        string currentPath = Directory.GetCurrentDirectory();

        return Path.Combine(currentPath, relativeFilePath);
    }

    [Theory]
    [InlineData(@"test.custom.2.txt", 3)]
    [InlineData(@"test.custom.3.txt", 61)]
    [InlineData(@"test.custom.txt", 316)]
    [InlineData(@"test.txt", 342190)]
    public void Should_Test_For_Path_To_File_Byte_Counts(string filePath, long expectedCount)
    {
        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        Result<long?> bytesCountResult = FileBasedWordCount.CountBytes(absoluteFilePath);

        // Assert
        Assert.NotNull(bytesCountResult.Data);
        Assert.Equal(expectedCount, bytesCountResult.Data);
    }

    [Theory]
    [InlineData(@"test.custom.2.txt", 1)]
    [InlineData(@"test.custom.3.txt", 1)]
    [InlineData(@"test.custom.txt", 10)]
    [InlineData(@"test.txt", 7145)]
    public void Should_Test_For_Path_To_File_Line_Counts(string filePath, long expectedCount)
    {
        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        Result<long?> linesCountResult = FileBasedWordCount.CountLines(absoluteFilePath);

        // Assert
        Assert.NotNull(linesCountResult.Data);
        Assert.Equal(expectedCount, linesCountResult.Data);
    }
}
