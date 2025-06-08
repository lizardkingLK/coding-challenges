namespace jpTool.Core.Tests;

public class TestsCustomJson
{
    #region Array
    [Theory]
    [InlineData(@"false.invalid.json", false)]
    [InlineData(@"false.invalid.variant.a.json", false)]
    [InlineData(@"false.valid.json", true)]
    [InlineData(@"false.valid.variant.a.json", true)]
    [InlineData(@"true.invalid.json", false)]
    [InlineData(@"true.invalid.variant.a.json", false)]
    [InlineData(@"true.valid.json", true)]
    [InlineData(@"true.valid.variant.a.json", true)]
    public void Should_Test_For_Array_With_Boolean(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\array\boolean\{filePath}";

        CommonTestWrapper(filePath, expectedResult);
    }

    [Theory]
    [InlineData(@"braces.invalid.json", false)]
    [InlineData(@"end.braces.invalid.json", false)]
    [InlineData(@"start.braces.invalid.json", false)]
    public void Should_Test_For_Array_With_Braces(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\array\braces\{filePath}";

        CommonTestWrapper(filePath, expectedResult);
    }

    [Theory]
    [InlineData(@"empty.valid.json", true)]
    [InlineData(@"spacious.valid.json", true)]
    public void Should_Test_For_Empty_Array_Literal(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\array\empty\{filePath}";

        CommonTestWrapper(filePath, expectedResult);
    }

    [Theory]
    [InlineData(@"nested.invalid.json", false)]
    [InlineData(@"nested.valid.json", true)]
    [InlineData(@"nested.valid.variant.a.json", true)]
    [InlineData(@"nested.valid.variant.b.json", true)]
    [InlineData(@"nested.valid.variant.c.json", true)]
    [InlineData(@"nested.valid.variant.d.json", true)]
    public void Should_Test_For_Nested_Array_Literal(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\array\nested\{filePath}";

        CommonTestWrapper(filePath, expectedResult);
    }

    [Theory]
    [InlineData(@"null.invalid.json", false)]
    [InlineData(@"null.invalid.variant.a.json", false)]
    [InlineData(@"null.valid.json", true)]
    [InlineData(@"null.valid.variant.a.json", true)]
    public void Should_Test_For_Null_Array_Literal(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\array\null\{filePath}";

        CommonTestWrapper(filePath, expectedResult);
    }

    [Theory]
    [InlineData(@"end.quotes.invalid.json", false)]
    [InlineData(@"end.quotes.invalid.variant.a.json", false)]
    [InlineData(@"quotes.invalid.json", false)]
    [InlineData(@"quotes.valid.json", true)]
    [InlineData(@"start.quotes.invalid.json", false)]
    [InlineData(@"start.quotes.invalid.variant.a.json", false)]
    public void Should_Test_For_Array_With_Quotes(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\array\quotes\{filePath}";

        CommonTestWrapper(filePath, expectedResult);
    }
    #endregion

    #region Json
    [Theory]
    [InlineData(@"false.invalid.json", true)]
    [InlineData(@"true.invalid.json", true)]
    public void Should_Test_For_Boolean_Json(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\json\boolean\{filePath}";

        CommonTestWrapper(filePath, expectedResult);
    }

    [Theory]
    [InlineData(@"comments.invalid.json", false)]
    public void Should_Test_For_Comments_Json(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\json\comments\{filePath}";

        CommonTestWrapper(filePath, expectedResult);
    }

    [Theory]
    [InlineData(@"empty.invalid.json", false)]
    [InlineData(@"spacious.invalid.json", false)]
    public void Should_Test_For_Empty_Json(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\json\empty\{filePath}";

        CommonTestWrapper(filePath, expectedResult);
    }

    [Theory]
    [InlineData(@"null.valid.json", true)]
    public void Should_Test_For_Null_Json(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\json\null\{filePath}";

        CommonTestWrapper(filePath, expectedResult);
    }

    [Theory]
    [InlineData(@"number.invalid.json", false)]
    [InlineData(@"number.invalid.variant.a.json", false)]
    [InlineData(@"number.invalid.variant.b.json", false)]
    [InlineData(@"number.valid.json", true)]
    [InlineData(@"number.valid.variant.a.json", true)]
    [InlineData(@"number.valid.variant.b.json", true)]
    public void Should_Test_For_Number_Json(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\json\number\{filePath}";

        CommonTestWrapper(filePath, expectedResult);
    }

    [Theory]
    [InlineData(@"text.invalid.json", false)]
    [InlineData(@"text.valid.json", true)]
    [InlineData(@"text.valid.variant.a.json", true)]
    public void Should_Test_For_Text_Json(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\json\text\{filePath}";

        CommonTestWrapper(filePath, expectedResult);
    }
    #endregion

    #region Object
    [Theory]
    [InlineData(@"false.invalid.json", false)]
    [InlineData(@"false.invalid.variant.a.json", false)]
    [InlineData(@"false.valid.json", true)]
    [InlineData(@"false.valid.variant.a.json", true)]
    [InlineData(@"true.invalid.json", false)]
    [InlineData(@"true.invalid.variant.a.json", false)]
    [InlineData(@"true.valid.json", true)]
    [InlineData(@"true.valid.variant.a.json", true)]
    public void Should_Test_For_Object_With_Boolean(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\object\boolean\{filePath}";

        CommonTestWrapper(filePath, expectedResult);
    }

    [Theory]
    [InlineData(@"empty.valid.json", true)]
    [InlineData(@"spacious.valid.json", true)]
    public void Should_Test_For_Empty_Object_Literal(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\object\empty\{filePath}";

        CommonTestWrapper(filePath, expectedResult);
    }

    [Theory]
    [InlineData(@"null.invalid.json", false)]
    [InlineData(@"null.invalid.variant.a.json", false)]
    [InlineData(@"null.invalid.variant.b.json", false)]
    [InlineData(@"null.invalid.variant.c.json", false)]
    [InlineData(@"null.valid.json", true)]
    [InlineData(@"null.valid.variant.a.json", true)]
    [InlineData(@"null.valid.variant.b.json", true)]
    [InlineData(@"null.valid.variant.c.json", true)]
    public void Should_Test_For_Null_Object_Literal(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\object\null\{filePath}";

        CommonTestWrapper(filePath, expectedResult);
    }

    [Theory]
    [InlineData(@"quotes.invalid.json", false)]
    [InlineData(@"quotes.invalid.variant.a.json", false)]
    [InlineData(@"quotes.invalid.variant.b.json", false)]
    [InlineData(@"quotes.valid.json", true)]
    [InlineData(@"quotes.valid.variant.a.json", true)]
    public void Should_Test_For_Quotes_Object_Literal(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\object\quotes\{filePath}";

        CommonTestWrapper(filePath, expectedResult);
    }
    #endregion

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
