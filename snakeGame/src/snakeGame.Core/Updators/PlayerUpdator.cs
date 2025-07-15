using snakeGame.Core.Abstractions;
using snakeGame.Core.State;

namespace snakeGame.Core.Updators;

public class PlayerUpdator : IPlay
{
    public IPlay? Next { get; init; }

    public required Manager Manager { get; init; }

    public required IOutput Output { get; init; }

    public void Play()
    {
        // INFO: Just testing
        // GetOutput(argumentsValidationResult.Data.Item4).Output(manager);

        ConsoleKeyInfo? read;
        Console.WriteLine("awaiting button press");
        while (true)
        {
            read = Console.ReadKey();
            if (read.Value.Key == null)
            {
                break;
            }
        }

        // TODO: run loop
        
        throw new NotImplementedException();
    }
}