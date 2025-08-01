namespace jpTool.Core;

using System.Text.RegularExpressions;

public static partial class RegularExpressions
{
    [GeneratedRegex(@"^(\\[\\""/bfnrt]{1}|(\\u[0-9aA-fF]{4}))$")]
    public static partial Regex RegexForEscapeSequence();

    [GeneratedRegex(@"^[-]{0,1}(0|([1-9]{1})([0-9]{0,}))(\.[0-9]{1,}){0,1}([eE][-+]{0,1}[0-9]{1,}){0,1}$")]
    public static partial Regex RegexForValidNumericLiteral();
}