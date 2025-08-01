namespace jpTool.Core.Tests;

using System.Text.RegularExpressions;

using static jpTool.Core.RegularExpressions;

[Collection("jpTool.Core.Tests")]
public class TestsRegexSyntax
{
    [Theory]
    [InlineData("\\\\", true)]
    [InlineData("\\\"", true)]
    [InlineData("\\b", true)]
    [InlineData("\\f", true)]
    [InlineData("\\n", true)]
    [InlineData("\\r", true)]
    [InlineData("\\t", true)]
    [InlineData("\\u0001", true)]
    [InlineData("\\ffff", false)]
    [InlineData("\\tyre", false)]
    [InlineData("abcd", false)]
    [InlineData("1234", false)]
    [InlineData("!@#$", false)]
    public void Should_Test_For_Escape_Sequence_Exact_Containment(string input, bool expectedResult)
    {
        // Arrange
        Regex escapeSequenceRegex = RegexForEscapeSequence();

        // Act
        bool actualResult = escapeSequenceRegex.IsMatch(input);

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [InlineData("0", true)]
    [InlineData("-0", true)]
    [InlineData("1", true)]
    [InlineData("5", true)]
    [InlineData("0.1", true)]
    [InlineData("0.01", true)]
    [InlineData("1.1", true)]
    [InlineData("1.01", true)]
    [InlineData("-1.01", true)]
    [InlineData("-0.01", true)]
    [InlineData("-1.01e1", true)]
    [InlineData("-1.01e-1", true)]
    [InlineData("-1.01e-0", true)]
    [InlineData("-1.01E-0", true)]
    [InlineData("-1.01E+0", true)]
    [InlineData("-1000.001E+0", true)]
    [InlineData("-1.01e+1", true)]
    [InlineData("-.001E+0", false)]
    [InlineData("-1.01e", false)]
    [InlineData("0x1.01", false)]
    [InlineData("0b1.01", false)]
    [InlineData("+0", false)]
    [InlineData("01", false)]
    [InlineData("01.", false)]
    [InlineData("01.1", false)]
    [InlineData("-01.1", false)]
    [InlineData("-001.1", false)]
    [InlineData("-", false)]
    [InlineData("-01.1.1", false)]
    public void Should_Test_For_Numeric_Literals(string input, bool expectedResult)
    {
        // Arrange
        Regex numericLiteralRegex = RegexForValidNumericLiteral();

        // Act
        bool actualResult = numericLiteralRegex.IsMatch(input);

        // Assert
        Assert.Equal(expectedResult, actualResult);
    }
}