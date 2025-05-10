namespace wcTool.Core
{
    internal static class Constants
    {
        internal const string ResponseFormat = " {0} {1}";
        internal static readonly char[] WordSeparator = new char[] { ' ', '\n', '\t', '\r' };
        internal const string ERROR_INVALID_COMMANDS = "error. command '{0}' is invalid";
        internal const string ERROR_INVALID_FILE_PATH = "error. invalid file path '{0}' was given";
        internal const string ERROR_INVALID_CONTENT = "error. input content are invalid";
        internal const string ERROR_FILE_IN_USE = "error. file cannot be read at the moment";
    }
}