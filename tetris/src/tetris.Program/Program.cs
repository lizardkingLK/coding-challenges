using tetris.Core;

namespace tetris.Program;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Tetris.Play(args);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
