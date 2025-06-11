namespace jpTool.Core.Tests;

using static Utility;

[Collection("jpTool.Core.Tests")]
public class TestsCustomJson
{
    #region Array
    [Theory]
    [InlineData(@"array.invalid.json", false)]
    [InlineData(@"array.invalid.variant.a.json", false)]
    [InlineData(@"array.invalid.variant.b.json", false)]
    [InlineData(@"array.valid.json", true)]
    [InlineData(@"array.valid.variant.a.json", true)]
    [InlineData(@"array.valid.variant.b.json", true)]
    public void Should_Test_For_Array_With_Array_Literals(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\array\array\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"backslash.value.valid.json", true)]
    public void Should_Test_For_Array_With_Backslash(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\object\backslash\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"false.invalid.json", false)]
    [InlineData(@"false.invalid.variant.a.json", false)]
    [InlineData(@"false.valid.json", true)]
    [InlineData(@"false.valid.variant.a.json", true)]
    [InlineData(@"true.invalid.json", false)]
    [InlineData(@"true.invalid.variant.a.json", false)]
    [InlineData(@"true.valid.json", true)]
    [InlineData(@"true.valid.variant.a.json", true)]
    public void Should_Test_For_Array_With_Boolean_Literals(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\array\boolean\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"braces.invalid.json", false)]
    [InlineData(@"end.braces.invalid.json", false)]
    [InlineData(@"start.braces.invalid.json", false)]
    public void Should_Test_For_Array_With_Braces(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\array\braces\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"empty.valid.json", true)]
    [InlineData(@"spacious.valid.json", true)]
    public void Should_Test_For_Array_But_Empty(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\array\empty\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"nested.invalid.json", false)]
    [InlineData(@"nested.valid.json", true)]
    [InlineData(@"nested.valid.variant.a.json", true)]
    [InlineData(@"nested.valid.variant.b.json", true)]
    [InlineData(@"nested.valid.variant.c.json", true)]
    [InlineData(@"nested.valid.variant.d.json", true)]
    public void Should_Test_For_Array_With_Nested_Array_Literals(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\array\nested\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"null.invalid.json", false)]
    [InlineData(@"null.invalid.variant.a.json", false)]
    [InlineData(@"null.valid.json", true)]
    [InlineData(@"null.valid.variant.a.json", true)]
    public void Should_Test_For_Array_With_Null_Literals(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\array\null\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"object.invalid.json", false)]
    [InlineData(@"object.invalid.variant.a.json", false)]
    [InlineData(@"object.invalid.variant.b.json", false)]
    [InlineData(@"object.valid.json", true)]
    [InlineData(@"object.valid.variant.a.json", true)]
    [InlineData(@"object.valid.variant.b.json", true)]
    public void Should_Test_For_Array_With_Object_Literals(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\array\object\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
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

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }
    #endregion

    #region Json
    [Theory]
    [InlineData(@"false.invalid.json", false)]
    [InlineData(@"true.invalid.json", false)]
    public void Should_Test_For_Json_With_Boolean_Literals(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\json\boolean\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"break.array.value.invalid.json", false)]
    [InlineData(@"break.object.key.invalid.json", false)]
    [InlineData(@"break.object.value.invalid.json", false)]
    public void Should_Test_For_Json_With_Breaks(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\json\breaks\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"comments.invalid.json", false)]
    public void Should_Test_For_Json_With_Comments(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\json\comments\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"empty.invalid.json", false)]
    [InlineData(@"spacious.invalid.json", false)]
    public void Should_Test_For_Json_But_Empty(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\json\empty\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"null.valid.json", false)]
    public void Should_Test_For_Json_With_Empty_Literals(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\json\null\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"number.invalid.json", false)]
    [InlineData(@"number.invalid.variant.a.json", false)]
    [InlineData(@"number.invalid.variant.b.json", false)]
    [InlineData(@"number.valid.json", false)]
    [InlineData(@"number.valid.variant.a.json", false)]
    [InlineData(@"number.valid.variant.b.json", false)]
    public void Should_Test_For_Json_With_Number_Literals(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\json\number\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"tab.array.value.invalid.json", false)]
    public void Should_Test_For_Json_With_Tabs(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\json\tabs\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"text.invalid.json", false)]
    [InlineData(@"text.valid.json", false)]
    [InlineData(@"text.valid.variant.a.json", false)]
    public void Should_Test_For_Json_With_Text(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\json\text\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }
    #endregion

    #region Object
    [Theory]
    [InlineData(@"array.invalid.json", false)]
    [InlineData(@"array.invalid.variant.a.json", false)]
    [InlineData(@"array.invalid.variant.b.json", false)]
    [InlineData(@"array.valid.json", true)]
    [InlineData(@"array.valid.variant.a.json", true)]
    [InlineData(@"array.valid.variant.b.json", true)]
    public void Should_Test_For_Object_With_Array_Literals(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\object\array\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"backslash.key.valid.json", true)]
    [InlineData(@"backslash.value.valid.json", true)]
    public void Should_Test_For_Object_With_Backslash(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\object\backslash\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"false.invalid.json", false)]
    [InlineData(@"false.invalid.variant.a.json", false)]
    [InlineData(@"false.valid.json", true)]
    [InlineData(@"false.valid.variant.a.json", true)]
    [InlineData(@"true.invalid.json", false)]
    [InlineData(@"true.invalid.variant.a.json", false)]
    [InlineData(@"true.valid.json", true)]
    [InlineData(@"true.valid.variant.a.json", true)]
    public void Should_Test_For_Object_With_Boolean_Literals(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\object\boolean\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"empty.valid.json", true)]
    [InlineData(@"spacious.invalid.json", false)]
    [InlineData(@"spacious.invalid.variant.a.json", false)]
    [InlineData(@"spacious.valid.json", true)]
    public void Should_Test_For_Object_But_Empty(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\object\empty\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"nested.invalid.json", false)]
    [InlineData(@"nested.valid.json", true)]
    [InlineData(@"nested.valid.variant.a.json", true)]
    [InlineData(@"nested.valid.variant.b.json", true)]
    [InlineData(@"nested.valid.variant.c.json", true)]
    [InlineData(@"nested.valid.variant.d.json", true)]
    public void Should_Test_For_Object_With_Nested_Object_Literals(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\object\nested\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
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
    public void Should_Test_For_Object_With_Null_Literals(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\object\null\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"number.invalid.json", false)]
    [InlineData(@"number.invalid.variant.a.json", false)]
    [InlineData(@"number.invalid.variant.b.json", false)]
    [InlineData(@"number.valid.json", true)]
    [InlineData(@"number.valid.variant.a.json", true)]
    public void Should_Test_For_Object_With_Number_Literals(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\object\number\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"object.invalid.json", false)]
    [InlineData(@"object.invalid.variant.a.json", false)]
    [InlineData(@"object.invalid.variant.b.json", false)]
    [InlineData(@"object.valid.json", true)]
    [InlineData(@"object.valid.variant.a.json", true)]
    [InlineData(@"object.valid.variant.b.json", true)]
    public void Should_Test_For_Object_With_Object_Literals(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\object\object\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }

    [Theory]
    [InlineData(@"quotes.invalid.json", false)]
    [InlineData(@"quotes.invalid.variant.a.json", false)]
    [InlineData(@"quotes.invalid.variant.b.json", false)]
    [InlineData(@"quotes.valid.json", true)]
    [InlineData(@"quotes.valid.variant.a.json", true)]
    public void Should_Test_For_Object_With_Quotes_Literals(string filePath, bool expectedResult)
    {
        filePath = @$"data\custom\object\quotes\{filePath}";

        // Arrange
        string absoluteFilePath = GetAbsoluteFilePath(filePath);

        // Act
        var actualResult = Validator.ValidateJsonFile(absoluteFilePath);

        // Assert
        Assert.Equal(expectedResult, actualResult.Data);
    }
    #endregion
}
