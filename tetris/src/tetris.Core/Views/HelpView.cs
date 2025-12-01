using tetris.Core.Abstractions;

namespace tetris.Core.Views;

public class HelpView : IView
{
    public string Message => """

    +----------------+
    |    TETRIS      |
    +----------------+

    USAGE           = tetris [OPTIONS] 

    OPTIONS
    
    help           = -h    | --help
    scores         = -s    | --scores
    interactive    = -it   | --interactive

    difficulty     = [-d   | --difficulty] <Difficulty-Level>
    game-mode      = [-gm  | --game-mode]  <Game-Mode>
    map-size       = [-ms  | --map-size]   <Map-Size>
    play-mode      = [-pm  | --play-mode]  <Play-Mode>
    

    Difficulty Levels


    -1 - Easy
    0  - Medium
    1  - Hard


    Game Modes


    0  - Classic (Score multiplier on difficulty)
    1  - Timed (Score multiplier on difficulty and time elapsed)


    Map Size


    0  - Normal (Normal Map Size)
    1  - Scaled (Doubled Map Size)


    Play Mode

    0  - Drop (Tetrominoes drops down with input)
    1  - Float (Tetrominoes waits for the input)
    
    """;

    public string Data => string.Empty;

    public string Error => string.Empty;
}