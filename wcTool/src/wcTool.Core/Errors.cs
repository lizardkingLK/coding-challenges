namespace wcTool.Core
{
    public class Errors
    {
        public const string INVALID_FILE_PATH = "error. invalid file path was given";
        public const string INCOMPLETE_ARGUMENTS = "error. arguments are incomplete";
        public const string INVALID_COMMANDS = "error. commands are invalid";

        public static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}