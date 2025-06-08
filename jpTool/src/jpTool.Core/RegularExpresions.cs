namespace jpTool.Core;

using System.Text.RegularExpressions;

internal static partial class RegularExpressions
{
    [GeneratedRegex(@"(([\n\r\t\s\b\f])|(\\u+[0-9aA-fF]{4}))")]
    internal static partial Regex RegexForSpecialAndWhiteSpace();

    [GeneratedRegex(@"^[-]{0,1}(0|([1-9]{1})([0-9]{0,}))(\.[0-9]{1,}){0,1}([eE][-]{0,1}[0-9]{1,}){0,1}$")]
    internal static partial Regex RegexForMatchScientificNumber();

    [GeneratedRegex(@"^(\"")(.){1,}(\1)$")]
    internal static partial Regex RegexForMatchQuotedLineOfText();
}