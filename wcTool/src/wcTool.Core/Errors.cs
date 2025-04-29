namespace wcTool.Core
{
    public class Errors
    {
        public const string INVALID_COMMANDS = "error. command '{0}' is invalid";
        public const string INVALID_FILE_PATH = "error. invalid file path '{0}' was given";
        public const string INCOMPLETE_ARGUMENTS = "error. arguments are incomplete";
        public const string FILE_IN_USE = "error. file cannot be read at the moment";

        public static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}