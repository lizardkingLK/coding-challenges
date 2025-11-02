using pong.Core.Abstractions;
using pong.Core.Enums;
using pong.Core.Library.DataStructures.NonLinear.HashMaps;
using pong.Core.State.Game;
using static pong.Core.Enums.PlayerSideEnum;

namespace pong.Core.State.Handlers;

public class ScoreManager(Output output)
{
    private const string InitialScore = "0";

    private readonly Output _output = output;

    private readonly HashMap<PlayerSideEnum, int> _scores = new(
           new(PlayerLeft, 0),
           new(PlayerRight, 0));

    private readonly HashMap<PlayerSideEnum, Position> _positions = new(
           new(PlayerLeft, new(0, output.Width / 4)),
           new(PlayerRight, new(0, 3 * output.Width / 4)));

    private readonly ConsoleColor _scoreColor = ConsoleColor.Cyan;

    public int Score(PlayerSideEnum playerSide)
    {
        int score = ++_scores[playerSide];
        _output.Draw(_positions[playerSide], score.ToString(), _scoreColor);

        return score;
    }

    public void Output()
    {
        _output.Draw(_positions[PlayerLeft], InitialScore, _scoreColor);
        _output.Draw(_positions[PlayerRight], InitialScore, _scoreColor);
    }
}