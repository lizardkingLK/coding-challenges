using pong.Core.Abstractions;

namespace pong.Core.Views;

public class HelpView : View
{
    public override string? Data => """

    +----------------+
    |      PONG      |
    +----------------+

    USAGE           = pong [OPTIONS] 

    OPTIONS
    
    help            = -h    | --help
    interactive     = -it   | --interactive

    game-mode       = [-gm  | --game-mode]     <Game-Mode>
    difficulty      = [-d   | --difficulty]    <Difficulty-Level>
    player-side     = [-ps  | --player-side]   <Player-Side>
    points-to-win   = [-ptw | --points-to-win] <Points-To-Win>
    

    Game Modes
    
    -1  - Automatic
    0   - Offline SinglePlayer
    1   - Offline MultiPlayer
    2   - Online (TBA)    


    Difficulty Levels
    
    -1 - Easy
    0  - Medium
    1  - Hard


    Player Side

    0 - Left Player Side
    1 - Right Player Side
    
    """;

    public override string? Message => string.Empty;

    public override string? Error => string.Empty;
}