namespace jpTool.Core;

using static Constants;

internal static class Values
{
    internal static readonly char[] valuePrefixes =
    [
        Negative,
        CharZero,
        CharOne,
        CharTwo,
        CharThree,
        CharFour,
        CharFive,
        CharSix,
        CharSeven,
        CharEight,
        CharNine,
        Quotes,
        OpenCurlyBrace,
        OpenSquareBrace,
        CharNullStart,
        CharTrueStart,
        CharFalseStart,
    ];

    internal static readonly char[] whitespaceCharacters =
    [
        FullCarriage,
        FullLineBreak,
        FullTabspace,
        FullSpace,
        FullBackspace,
        FullFormFeed,
    ];

    internal static readonly char[] numberValueEndings =
    [
        Comma,
        CloseCurlyBrace,
        CloseSquareBrace,
        FullCarriage,
        FullLineBreak,
        FullTabspace,
        FullSpace,
        FullBackspace,
        FullFormFeed,
    ];
}
