namespace snakeGame.Core.Tests;

using static snakeGame.Core.Utility;

[Collection("snakeGame.Core.Tests")]
public class TestWallsAndPaths
{
    [Theory]
    [InlineData(1, "#")]
    [InlineData(2, "##")]
    [InlineData(3, "###")]
    [InlineData(21, "####################")]
    public void Should_Test_For_Wall_Generation(int width, string expectedWall)
    {
        // Act
        char[] horizontalWallArray = GetHorizontalWall(width);

        // Assert

        Assert.Equal(expectedWall, string.Join(null, horizontalWallArray));
    }

    [Theory]
    [InlineData(1, "")]
    [InlineData(2, "")]
    [InlineData(3, "# #")]
    [InlineData(21, "#                  #")]
    public void Should_Test_For_Path_Generation(int width, string expectedPath)
    {
        // Act
        char[] horizontalPathArray = GetHorizontalPath(width);

        // Assert
        Assert.Equal(expectedPath, string.Join(null, horizontalPathArray));
    }
}